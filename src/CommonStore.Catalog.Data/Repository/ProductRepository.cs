using Microsoft.EntityFrameworkCore;
using CommonStore.Catalog.Domain;
using CommonStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonStore.Catalog.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        //UnitOfWork reflects _context
        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Product>> GetAll()
        {
            //AsNoTracking uses less resources from EF
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByCategory(int Code)
        {
            return await _context.Produtos.AsNoTracking().Include(p => p.Category).Where(c => c.Category.Code == Code).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categorias.AsNoTracking().ToListAsync();
        }

        public void Add(Product produto)
        {
            _context.Produtos.Add(produto);
        }

        public void Update(Product produto)
        {
            _context.Produtos.Update(produto);
        }

        public void Add(Category categoria)
        {
            _context.Categorias.Add(categoria);
        }

        public void Update(Category categoria)
        {
            _context.Categorias.Update(categoria);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}