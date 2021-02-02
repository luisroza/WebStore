using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommonStore.Catalog.Application.Services;

namespace CommonStore.WebApp.MVC.Controllers
{
    public class DisplayController : Controller
    {
        private readonly IProductAppService _productAppService;

        public DisplayController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("display")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAll());
        }

        [HttpGet]
        [Route("product-detail/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            return View(await _productAppService.GetById(id));
        }
    }
}