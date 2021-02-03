using System.Threading.Tasks;

namespace WebStore.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}