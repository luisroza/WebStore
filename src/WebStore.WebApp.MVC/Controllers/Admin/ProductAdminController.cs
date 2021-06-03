using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Catalog.Application.Services;
using WebStore.Catalog.Application.ViewModels;

namespace WebStore.WebApp.MVC.Controllers.Admin
{
    [Authorize]
    [Route("admin-products")]
    public class ProductAdminController : Controller
    {
        private readonly IProductAppService _productAppService;

        public ProductAdminController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("product-list")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAll());
        }

        [Route("new-product")]
        public async Task<IActionResult> NewProduct()
        {
            return View(await GetCategories(new ProductViewModel()));
        }

        [Route("new-product")]
        [HttpPost]
        public async Task<IActionResult> NewProduct(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return View(await GetCategories(productViewModel));

            await _productAppService.AddProduct(productViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit-product")]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            return View(await GetCategories(await _productAppService.GetById(id)));
        }

        [HttpPost]
        [Route("edit-product")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductViewModel productViewModel)
        {
            var product = await _productAppService.GetById(id);
            productViewModel.StockQuantity= product.StockQuantity;

            ModelState.Remove("StockQuantity");
            if (!ModelState.IsValid) return View(await GetCategories(productViewModel));

            await _productAppService.UpdateProduct(productViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("products-update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id)
        {
            return View("Stock", await _productAppService.GetById(id));
        }

        [HttpPost]
        [Route("products-update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id, int quantity)
        {
            if (quantity > 0)
            {
                await _productAppService.ReplenishStock(id, quantity);
            }
            else
            {
                await _productAppService.DecreaseStock(id, quantity);
            }

            return View("Index", await _productAppService.GetAll());
        }

        private async Task<ProductViewModel> GetCategories(ProductViewModel product)
        {
            product.Categories = await _productAppService.GetCategories();
            return product;
        }
    }
}