using WebStore.Core.Data;

namespace WebStore.Payments.Business
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void Add(Payment payment);
        void AddTransaction(Transaction transaction);
    }
}
