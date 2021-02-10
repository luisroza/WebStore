using System.Threading.Tasks;
using WebStore.Core.DomainObjects.DTO;

namespace WebStore.Payments.Business
{
    public interface IPaymentService
    {
        Task<Transaction> CheckOut(OrderPayment orderPayment);
    }
}
