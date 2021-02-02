using Microsoft.EntityFrameworkCore;
using CommonStore.Core.Data;
using CommonStore.Sales.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonStore.Sales.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SalesContext _context;

        public OrderRepository(SalesContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Order> GetById(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetListByCustomerId(Guid customerId)
        {
            return await _context.Orders.AsNoTracking().Where(p => p.CustomerId == customerId).ToListAsync();
        }

        public async Task<Order> GetDraftOrderByCustomerId(Guid customerId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(p => p.CustomerId == customerId && p.OrderStatus == OrderStatus.Draft);
            if (order == null) return null;

            await _context.Entry(order)
                .Collection(i => i.OrderLines).LoadAsync();

            if (order.VoucherId != null)
            {
                await _context.Entry(order)
                    .Reference(i => i.Voucher).LoadAsync();
            }

            return order;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }


        public async Task<OrderLine> GetOrderLineById(Guid id)
        {
            return await _context.OrderLines.FindAsync(id);
        }

        public async Task<OrderLine> GetOrderLineByOrder(Guid orderId, Guid productId)
        {
            return await _context.OrderLines.FirstOrDefaultAsync(p => p.ProductId == productId && p.OrderId == orderId);
        }

        public void AddOrderLine(OrderLine orderLine)
        {
            _context.OrderLines.Add(orderLine);
        }

        public void UpdateOrderLine(OrderLine orderLine)
        {
            _context.OrderLines.Update(orderLine);
        }

        public void RemoveOrderLine(OrderLine orderLine)
        {
            _context.OrderLines.Remove(orderLine);
        }

        public async Task<Voucher> GetVoucherByCode(string Code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Code == Code);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}