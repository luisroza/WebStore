using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Core.DomainObjects.DTO;
using WebStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace WebStore.Payments.Business.Events
{
    public class PaymentEventHandler : INotificationHandler<OrderStockConfirmedEvent>
    {
        private readonly IPaymentService _paymentService;

        public PaymentEventHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Handle(OrderStockConfirmedEvent message, CancellationToken cancellationToken)
        {
            var orderPayment = new OrderPayment
            {
                OrderId = message.OrderId,
                CustomerId = message.CustomerId,
                Total = message.Total,
                CardName = message.CardName,
                CardNumber = message.CardNumber,
                CardExpirationDate = message.CardExpirationDate,
                CardVerificationCode = message.CardVerificationCode
            };

            await _paymentService.CheckOut(orderPayment);
        }
    }
}
