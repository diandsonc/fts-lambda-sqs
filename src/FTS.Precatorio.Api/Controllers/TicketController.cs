using System.Threading.Tasks;
using FTS.Precatorio.Domain.Notifications;
using FTS.Precatorio.Domain.Tickets.Services;
using Microsoft.AspNetCore.Mvc;

namespace FTS.Precatorio.Api.Controllers
{
    [Route("api/[controller]")]
    public class TicketController : BaseController
    {
        private TicketService _ticketService;

        public TicketController(IDomainNotification notifications, TicketService ticketService) : base(notifications)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _ticketService.GetAll();

            return Response(data);
        }
    }
}
