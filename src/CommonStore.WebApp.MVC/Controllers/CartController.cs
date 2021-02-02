using MediatR;
using Microsoft.AspNetCore.Mvc;
using CommonStore.Catalog.Application.Services;
using CommonStore.Core.Communication.Mediator;
using CommonStore.Core.Messages.CommonMessages.Notifications;
using CommonStore.Sales.Application.Commands;
using System;
using System.Threading.Tasks;

namespace CommonStore.WebApp.MVC.Controllers
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
        public async Task<IActionResult> AdicionarItem(Guid id, int quantity)
        {
            var product = await _productAppService.GetById(id);
            if (product == null) return BadRequest();

            if (product.StockQuantity < quantity)
            {
                TempData["Erro"] = "Produto com estoque insuficiente";
                return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
            }

            var command = new AddOrderLineCommand(CustomerId, product.Id, product.Name, quantity, product.Price);
            await _mediatorHandler.SendCommand(command);

            if (OperationIsValid())
            {
                return RedirectToAction("Index");
            }

            TempData["Erro"] = "Produto indisponível";
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }
    }
}
