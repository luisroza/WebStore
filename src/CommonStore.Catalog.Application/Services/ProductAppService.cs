using AutoMapper;
using CommonStore.Catalog.Application.ViewModels;
using CommonStore.Catalog.Domain;
using CommonStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommonStore.Catalog.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public ProductAppService(IProductRepository productRepository, 
                                 IMapper mapper, 
                                 IStockService stockService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _stockService = stockService;
        }

        public async Task<IEnumerable<ProductViewModel>> GetByCategory(int Code)
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetByCategory(Code));
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAll());
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await _productRepository.GetCategories());
        }

        public async Task AddProduct(ProductViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Product>(produtoViewModel);
            _productRepository.Add(produto);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task UpdateProduct(ProductViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Product>(produtoViewModel);
            _productRepository.Update(produto);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task<ProductViewModel> DecreaseStock(Guid id, int Quantity)
        {
            if (!_stockService.DecreaseStock(id, Quantity).Result)
            {
                throw new DomainException("Error, stock not updated");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<ProductViewModel> ReplenishStock(Guid id, int Quantity)
        {
            if (!_stockService.ReplenishStock(id, Quantity).Result)
            {
                throw new DomainException("Error, stock not replenished");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}