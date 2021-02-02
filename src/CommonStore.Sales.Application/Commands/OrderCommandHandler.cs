using CommonStore.Core.Communication.Mediator;
using CommonStore.Core.Messages;
using CommonStore.Core.Messages.CommonMessages.Notifications;
using CommonStore.Sales.Domain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommonStore.Sales.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddOrderLineCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public OrderCommandHandler(IOrderRepository orderRepository, IMediatorHandler mediatorHandler)
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
            }
            else
            {
                var pedidoItemExistente = order.ExistentOrderLine(orderLine);
                order.AddOrderLine(orderLine);

                if (pedidoItemExistente)
                    _orderRepository.UpdateOrderLine(order.OrderLines.FirstOrDefault(p => p.ProductId == orderLine.ProductId));
                else
                    _orderRepository.AddOrderLine(orderLine);
            }
            
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
