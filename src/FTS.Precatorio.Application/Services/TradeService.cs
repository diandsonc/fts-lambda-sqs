using System;
using System.Collections.Generic;
using FTS.Precatorio.Application.ViewModels.Trade;
using FTS.Precatorio.Application.Services.Interfaces;
using FTS.Precatorio.Domain.Core.Enum;
using FTS.Precatorio.Domain.Core.SQS;
using FTS.Precatorio.Domain.Trade;
using FTS.Precatorio.Domain.Trade.Repository;
using System.Text.Json;
using System.Threading.Tasks;

namespace FTS.Precatorio.Application.Services
{
    public class TradeService : IAppServiceBase
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly ISQS _sqs;

        public TradeService(ISQS sqs, ITradeRepository tradeRepository)
        {
            _sqs = sqs;
            _tradeRepository = tradeRepository;
        }

        public void Add(TradeViewModel tradeViewModel)
        {
            var messsage = JsonSerializer.Serialize(new { Controller = "trades", Action = "insert", tradeViewModel.Id });

            _sqs.SendMessage(Queues.TRADE_QUEUE, messsage);
        }

        public void Add(Trade trade)
        {
            _tradeRepository.Add(trade);
        }

        public async Task<TradeViewModel> GetTradeById(Guid tradeId)
        {
            if (tradeId != Guid.Empty)
            {
                var dados = await _tradeRepository.GetById(tradeId);

                return TradeViewModel.Map(dados);
            }

            return null;
        }

        public void SetGroupId(Guid controlGroupId)
        {
            _tradeRepository.SetGroupId(controlGroupId);
        }

        public void IgnoreGroup(bool ignore)
        {
            _tradeRepository.IgnoreGroup = ignore;
        }

        public void Dispose()
        {
            _tradeRepository.Dispose();
        }
    }
}