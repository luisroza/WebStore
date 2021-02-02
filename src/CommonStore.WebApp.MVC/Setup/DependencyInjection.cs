using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CommonStore.Catalog.Application.Services;
using CommonStore.Catalog.Data;
using CommonStore.Catalog.Data.Repository;
using CommonStore.Catalog.Domain;
using CommonStore.Catalog.Domain.Events;
using CommonStore.Core.Communication.Mediator;
using CommonStore.Core.Messages.CommonMessages.Notifications;
using CommonStore.Sales.Application.Commands;
using CommonStore.Sales.Data;
using CommonStore.Sales.Data.Repository;
using CommonStore.Sales.Domain;

namespace CommonStore.WebApp.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator (wanna be BUS)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();

            services.AddScoped<INotificationHandler<ProductLowStockEvent>, ProductEventHandler>();

            // Sales
            services.AddScoped<IRequestHandler<AddOrderLineCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<SalesContext>();
        }
    }
}