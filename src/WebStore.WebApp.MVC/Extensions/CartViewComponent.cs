using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebStore.Sales.Application.Queries;

namespace WebStore.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IOrderQueries _orderQueries;

        //TODO: get logged user
        protected Guid CustomerId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

        public CartViewComponent(IOrderQueries orderQueries)
        {
            _orderQueries = orderQueries;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await _orderQueries.GetCustomerCart(CustomerId);
            var items = cart?.Lines.Count ?? 0;

            return View(items);
        }
    }
}
