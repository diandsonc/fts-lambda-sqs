using System;
using System.Threading.Tasks;
using FTS.Precatorio.Domain.Notifications;
using FTS.Precatorio.Domain.Trade.Services;
using FTS.Precatorio.Dto.Trade;
using Microsoft.AspNetCore.Mvc;

namespace FTS.Precatorio.Api.Controllers
{
    [Route("api/[controller]")]
    public class TradeController : BaseController
    {
        private TradeService _tradeService;

        public TradeController(IDomainNotification notifications, TradeService tradeService) : base(notifications)
        {
            _tradeService = tradeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _tradeService.GetTradeById(id);

            return Response(null);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TradeViewModel tradeViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotificateErrorInvalidModel();

                return Response();
            }

            // _tradeService.AddToQueue(tradeViewModel.Map());
            _tradeService.Add(tradeViewModel.Map()).Wait();

            return Response(true);
        }
    }
}