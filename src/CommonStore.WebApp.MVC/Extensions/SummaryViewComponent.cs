using MediatR;
using Microsoft.AspNetCore.Mvc;
using CommonStore.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace CommonStore.WebApp.MVC.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly DomainNotificationHandler _notifications;

        public SummaryViewComponent(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notifications = await Task.FromResult(_notifications.GetNotifications());
            notifications.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Value));

            return View();
        }

    }
}
