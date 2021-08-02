using System;
using FTS.Precatorio.Application.Services;
using FTS.Precatorio.Application.ViewModels.Trade;
using Microsoft.AspNetCore.Mvc;
using Service.Core.Controllers;

namespace Service.Trade.Controllers
{
    [Route("api/[controller]")]
    public class TradeController : BaseController
    {
        private TradeService _tradeService;

        public TradeController(TradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var data = _tradeService.FindTrade(id);

            return Response(data);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TradeViewModel tradeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Response();
            }

            _tradeService.Add(tradeViewModel);

            return Response(true);
        }
    }
}
