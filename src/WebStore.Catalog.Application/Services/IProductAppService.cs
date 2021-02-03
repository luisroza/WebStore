using WebStore.Catalog.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebStore.Catalog.Application.Services
{
    public interface IProductAppService : IDisposable
    {
        Task<IEnumerable<ProductViewModel>> GetByCategory(int Code);
        Task<ProductViewModel> GetById(Guid id);
        Task<IEnumerable<ProductViewModel>>  GetAll();
        Task<IEnumerable<CategoryViewModel>> GetCategories();

        Task AddProduct(ProductViewModel productViewModel);
        Task UpdateProduct(ProductViewModel productViewModel);

        Task<ProductViewModel> DecreaseStock(Guid id, int quantity);
        Task<ProductViewModel> ReplenishStock (Guid id, int quantity);
    }
}