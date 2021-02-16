using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Core.Communication.Mediator;
using WebStore.Core.Messages.CommonMessages.Notifications;
using WebStore.Sales.Application.Queries;

namespace WebStore.WebApp.MVC.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderQueries _orderQuery;

        public OrderController(IOrderQueries orderQuery, INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler)
            : base(notifications, mediatorHandler)
        {
            _orderQuery = orderQuery;
        }

        [Route("my-orders")]
        public async Task<IActionResult> Index()
        {
            return View(await _orderQuery.GetCustomerOrders(CustomerId));
        }
    }
}
