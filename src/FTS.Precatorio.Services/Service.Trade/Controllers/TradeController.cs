using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public TradeController() { }

        public TradeController(TradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _tradeService.GetTradeById(id);

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
