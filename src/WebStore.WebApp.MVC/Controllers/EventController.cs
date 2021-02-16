using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebStore.Core.Data.EventSourcing;

namespace WebStore.WebApp.MVC.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public EventController(IEventSourcingRepository eventSourcingRepository)
        {
            _eventSourcingRepository = eventSourcingRepository;
        }

        [HttpGet("events/{id:guid}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var events = await _eventSourcingRepository.GetEvents(id);
            return View(events);
        }
    }
}
