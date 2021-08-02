using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FTS.Precatorio.Application.Core.Interfaces;
using FTS.Precatorio.Application.ViewModels.Trade;
using FTS.Precatorio.Domain.Core.Enum;
using FTS.Precatorio.Domain.Core.SQS;
using FTS.Precatorio.Domain.Trade;
using FTS.Precatorio.Domain.Trade.Repository;
using FTS.Precatorio.Infrastructure.Utility;
using System.Text.Json;

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
            var command = tradeViewModel.Map();
            var messsage = JsonSerializer.Serialize(new { Controller = "trades", Action = "insert", command.Id });

            _sqs.SendMessage(Queues.TRADE_QUEUE, messsage);
        }

        public IEnumerable<TradeViewModel> FindTrade(Guid tradeId)
        {
            if (tradeId != Guid.Empty)
            {
                Expression<Func<Trade, bool>> predicate = null;

                predicate = ExpressionExtension.Query<Trade>();
                predicate = predicate.And(it => tradeId == it.Id);

                var dados = _tradeRepository.FindTrade(predicate);

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