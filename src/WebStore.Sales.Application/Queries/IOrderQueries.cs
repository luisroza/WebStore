using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Sales.Application.Queries.ViewModels;

namespace WebStore.Sales.Application.Queries
{
    public interface IOrderQueries
    {
        Task<CartViewModel> GetCustomerCart(Guid customerId);
        Task<IEnumerable<OrderViewModel>> GetCustomerOrders(Guid customerId);
    }
}
