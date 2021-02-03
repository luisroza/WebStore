using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Core.Data;

namespace WebStore.Catalog.Domain
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid id);
        Task<IEnumerable<Product>> GetByCategory(int Code);
        Task<IEnumerable<Category>> GetCategories();

        void Add(Product produto);
        void Update(Product produto);

        void Add(Category categoria);
        void Update(Category categoria);
    }
}