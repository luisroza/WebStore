using MediatR;
using Microsoft.AspNetCore.Mvc;
using CommonStore.Core.Communication.Mediator;
using CommonStore.Core.Messages.CommonMessages.Notifications;
using System;

namespace CommonStore.WebApp.MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        protected BaseController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        protected Guid CustomerId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d63");

        protected bool OperationIsValid()
        {
            return !_notifications.HasNotification();
        }

        protected void ErrorNotification(string Code, string message)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(Code, message));
        }
    }
}
