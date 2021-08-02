using System;

namespace FTS.Precatorio.Domain.Trade.Commands
{
    public class CreateTradeCommand : BaseTradeCommand
    {
        public string Code { get; protected set; }

        public CreateTradeCommand(Guid? id, string code)
        {
            Id = id ?? Guid.NewGuid();
            Code = code;
        }
    }
}