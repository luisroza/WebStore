using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebStore.Catalog.Application.Services;
using WebStore.Core.Communication.Mediator;
using WebStore.Core.Messages.CommonMessages.Notifications;
using WebStore.Sales.Application.Commands;
using System;
using System.Threading.Tasks;

namespace WebStore.WebApp.MVC.Controllers
{
    public class CartController : BaseController
    {
        private readonly IProductAppService _productAppService;
        private readonly IMediatorHandler _mediatorHandler;

        public CartController(INotificationHandler<DomainNotification> notifications, IProductAppService productAppService, IMediatorHandler mediatorHandler)
            : base(notifications, mediatorHandler)
        {
            _productAppService = productAppService;
            _mediatorHandler = mediatorHandler;
        } 

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("my-cart")]
        public async Task<IActionResult> AddOrderLine(Guid id, int quantity)
        {
            var product = await _productAppService.GetById(id);
            if (product == null) return BadRequest();

            if (product.StockQuantity < quantity)
            {
                TempData["Error"] = "Product stock not enough";
                return RedirectToAction("ProductDetail", "Display", new { id });
            }

            var command = new AddOrderLineCommand(CustomerId, product.Id, product.Name, quantity, product.Price);
            await _mediatorHandler.SendCommand(command);

            if (OperationIsValid())
            {
                return RedirectToAction("Index");
            }

            TempData["Errors"] = GetErrorMessages();
            return RedirectToAction("ProductDetail", "Display", new { id });
        }
    }
}
