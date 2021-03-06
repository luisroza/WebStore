﻿using EventSourcing;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Catalog.Application.Services;
using WebStore.Catalog.Data;
using WebStore.Catalog.Data.Repository;
using WebStore.Catalog.Domain;
using WebStore.Catalog.Domain.Events;
using WebStore.Core.Communication.Mediator;
using WebStore.Core.Data.EventSourcing;
using WebStore.Core.Messages.CommonMessages.IntegrationEvents;
using WebStore.Core.Messages.CommonMessages.Notifications;
using WebStore.Payments.AntiCorruption;
using WebStore.Payments.Business;
using WebStore.Payments.Business.Events;
using WebStore.Payments.Data;
using WebStore.Payments.Data.Repository;
using WebStore.Sales.Application.Commands;
using WebStore.Sales.Application.Events;
using WebStore.Sales.Application.Queries;
using WebStore.Sales.Data;
using WebStore.Sales.Data.Repository;
using WebStore.Sales.Domain;

namespace WebStore.WebApp.MVC.Setup
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
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<SalesContext>();

            services.AddScoped<IRequestHandler<AddOrderLineCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateOrderLineCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveOrderLineCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyVoucherOrderCommand, bool>, OrderCommandHandler>();

            services.AddScoped<INotificationHandler<DraftOrderStartedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<UpdateOrderEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<AddOrderLineEvent>, OrderEventHandler>();

            // Payment
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICreditCardPaymentFacade, CreditCardPaymentFacade>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, ConfigurationManager>();
            services.AddScoped<PaymentContext>();
            services.AddScoped<INotificationHandler<OrderStockConfirmedEvent>, PaymentEventHandler>();

            // Event Sourcing
            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();
        }
    }
}