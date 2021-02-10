using System.Threading.Tasks;
using WebStore.Core.Communication.Mediator;
using WebStore.Core.DomainObjects.DTO;
using WebStore.Core.Messages.CommonMessages.IntegrationEvents;
using WebStore.Core.Messages.CommonMessages.Notifications;

namespace WebStore.Payments.Business
{
    public class PaymentService : IPaymentService
    {
        private readonly ICreditCardPaymentFacade _creditCardPaymentFacade;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PaymentService(ICreditCardPaymentFacade creditCardPaymentFacade, IPaymentRepository paymentRepository
            , IMediatorHandler mediatorHandler)
        {
            _creditCardPaymentFacade = creditCardPaymentFacade;
            _paymentRepository = paymentRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Transaction> CheckOut(OrderPayment orderPayment)
        {
            var order = new Order
            {
                Id = orderPayment.OrderId,
                Amount = orderPayment.Total
            };

            var payment = new Payment
            {
                Amount = orderPayment.Total,
                CardName = orderPayment.CardName,
                CardNumber = orderPayment.CardNumber,
                CardExpirationDate = orderPayment.CardExpirationDate,
                CardVerificationCode = orderPayment.CardVerificationCode,
                OrderId = orderPayment.OrderId
            };

            var transaction = _creditCardPaymentFacade.CheckOut(order, payment);

            if (transaction.TransactionStatus == TransactionStatus.Paid)
            {
                payment.AddEvent(new CheckOutEvent(order.Id, orderPayment.CustomerId, transaction.PaymentId, transaction.Id, order.Amount));

                _paymentRepository.Add(payment);
                _paymentRepository.AddTransaction(transaction);

                await _paymentRepository.UnitOfWork.Commit();
                return transaction;
            }

            await _mediatorHandler.PublishNotification(new DomainNotification("payment", "Payment rejected by the Credit Card issuer"));
            await _mediatorHandler.PublishEvent(new PaymentRejectedEvent(order.Id, orderPayment.CustomerId, transaction.PaymentId, transaction.Id, order.Amount));

            return transaction;
        }
    }
}
