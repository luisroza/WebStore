using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Core.Communication.Mediator;
using WebStore.Core.Messages;
using WebStore.Core.Messages.CommonMessages.Notifications;
using WebStore.Sales.Application.Events;
using WebStore.Sales.Domain;

namespace WebStore.Sales.Application.Commands
{
    public class CommandHandler : IRequestHandler<AddOrderLineCommand, bool>,
                                   IRequestHandler<ApplyVoucherOrderCommand, bool>,
                                   IRequestHandler<RemoveOrderLineCommand, bool>,
                                   IRequestHandler<UpdateOrderLineCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public CommandHandler(IOrderRepository orderRepository, IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AddOrderLineCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerId(message.CustomerId);
            var orderLine = new OrderLine(message.ProductId, message.Name, message.Quantity, message.UnitPrice);
            
            if (order == null)
            {
                order = Order.OrderFactory.NewDraftOrder(message.CustomerId);
                order.AddOrderLine(orderLine);

                _orderRepository.Add(order);
                order.AddEvent(new DraftOrderStartedEvent(order.CustomerId, order.Id));
            }
            else
            {
                var existentOrderLine = order.ExistentOrderLine(orderLine);
                order.AddOrderLine(orderLine);

                if (existentOrderLine)
                {
                    _orderRepository.UpdateOrderLine(order.OrderLines.FirstOrDefault(p => p.ProductId == orderLine.ProductId));
                }
                else
                {
                    _orderRepository.AddOrderLine(orderLine);
                }

                order.AddEvent(new UpdateOrderEvent(order.CustomerId, order.Id, order.TotalPrice));
            }
            
            order.AddEvent(new AddOrderLineEvent(order.CustomerId, order.Id, message.ProductId, message.Name, message.UnitPrice, message.Quantity));
            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ApplyVoucherOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerId(message.CustomerId);
            if (order == null)
            {
                //Customer will receive all notifications
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
                return false;
            }

            var voucher = await _orderRepository.GetVoucherByCode(message.VoucherCode);
            if (voucher == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Voucher not found"));
                return false;
            }

            var voucherValidation = order.ApplyVoucher(voucher);
            if (!voucherValidation.IsValid)
            {
                foreach (var error in voucherValidation.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }
                return false;
            }

            order.AddEvent(new UpdateOrderEvent(order.CustomerId, order.Id, order.TotalPrice));
            order.AddEvent(new VoucherAppliedOrderEvent(message.CustomerId, order.Id, voucher.Id));

            _orderRepository.Update(order);
            // TODO: update voucher on DB

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoveOrderLineCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerId(message.CustomerId);
            if (order == null)
            {
                //Customer will receive all notifications
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
                return false;
            }

            var orderLine = await _orderRepository.GetOrderLineByOrder(order.Id, message.ProductId);
            if (order != null && !order.ExistentOrderLine(orderLine))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order Line not found"));
                return false;
            }

            order.RemoveOrderLine(orderLine);
            order.AddEvent(new UpdateOrderEvent(order.CustomerId, order.Id, order.TotalPrice));
            order.AddEvent(new RemoveOrderLineEvent(message.CustomerId, order.Id, message.ProductId));

            _orderRepository.RemoveOrderLine(orderLine);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(UpdateOrderLineCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerId(message.CustomerId);
            if (order == null)
            {
                //Customer will receive all notifications
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
                return false;
            }

            var orderLine = await _orderRepository.GetOrderLineByOrder(order.Id, message.ProductId);
            if (!order.ExistentOrderLine(orderLine))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order Line not found"));
                return false;
            }

            order.UpdateUnits(orderLine, message.Quantity);

            order.AddEvent(new UpdateOrderEvent(order.CustomerId, order.Id, order.TotalPrice));
            order.AddEvent(new UpdateProductOrderEvent(order.CustomerId, order.Id, message.ProductId, message.Quantity));

            _orderRepository.UpdateOrderLine(orderLine);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
