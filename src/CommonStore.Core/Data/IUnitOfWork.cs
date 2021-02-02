using System.Threading.Tasks;

namespace CommonStore.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}