using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebStore.Catalog.Application.Services;
using WebStore.Core.Communication.Mediator;
using WebStore.Core.Messages.CommonMessages.Notifications;
using WebStore.Sales.Application.Commands;
using System;
using System.Threading.Tasks;
using WebStore.Sales.Application.Queries;
using WebStore.Sales.Application.Queries.ViewModels;

namespace WebStore.WebApp.MVC.Controllers
{
    [Route("my-cart")]
    public class CartController : BaseController
    {
        private readonly IProductAppService _productAppService;
        private readonly IOrderQueries _orderQueries;
        private readonly IMediatorHandler _mediatorHandler;

        public CartController(INotificationHandler<DomainNotification> notifications,
                              IProductAppService productAppService,
                              IMediatorHandler mediatorHandler,
                              IOrderQueries orderQueries)
                                : base(notifications, mediatorHandler)
        {
            _productAppService = productAppService;
            _orderQueries = orderQueries;
            _mediatorHandler = mediatorHandler;
        } 

        public async Task<IActionResult> Index()
        {
            return View(await _orderQueries.GetCustomerCart(CustomerId));
        }

        [HttpPost]
        [Route("add-item")]
        public async Task<IActionResult> AddItem(Guid id, int quantity)
        {
            var product = await _productAppService.GetById(id);
            if (product == null) return BadRequest();

            if (product.StockQuantity < quantity)
            {
                TempData["Error"] = "Product stock is not enough";
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

        [HttpPost]
        [Route("remove-item")]
        public async Task<IActionResult> RemoveItem(Guid id)
        {
            var product = await _productAppService.GetById(id);
            if (product == null) return BadRequest();

            var command = new RemoveOrderLineCommand(CustomerId, id);
            await _mediatorHandler.SendCommand(command);

            if (OperationIsValid())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQueries.GetCustomerCart(CustomerId));
        }

        [HttpPost]
        [Route("update-item")]
        public async Task<IActionResult> UpdateItem(Guid id, int quantity)
        {
            var product = await _productAppService.GetById(id);
            if (product == null) return BadRequest();

            var command = new UpdateOrderLineCommand(CustomerId, id, quantity);
            await _mediatorHandler.SendCommand(command);

            if (OperationIsValid())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQueries.GetCustomerCart(CustomerId));
        }

        [HttpPost]
        [Route("apply-voucher")]
        public async Task<IActionResult> ApplyVoucher(string voucherCode)
        {
            var command = new ApplyVoucherOrderCommand(CustomerId, voucherCode);
            await _mediatorHandler.SendCommand(command);

            if (OperationIsValid())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQueries.GetCustomerCart(CustomerId));
        }

        [Route("order-summary")]
        public async Task<IActionResult> OrderSummary()
        {
            return View(await _orderQueries.GetCustomerCart(CustomerId));
        }

        [HttpGet]
        [Route("start-order")]
        public async Task<IActionResult> StartOrder(CartViewModel cartViewModel)
        {
            var cart = await _orderQueries.GetCustomerCart(CustomerId);

            var command = new StartOrderCommand(cart.OrderId, CustomerId, cart.TotalPrice, cartViewModel.Payment.CardName,
                cartViewModel.Payment.CardNumber, cartViewModel.Payment.CardExpirationDate, cartViewModel.Payment.CardVerificationCode);

            await _mediatorHandler.SendCommand(command);

            if (OperationIsValid())
            {
                return RedirectToAction("Index", "Order");
            }

            return View("OrderSummary", await _orderQueries.GetCustomerCart(CustomerId));
        }
    }
}
