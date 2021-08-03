using System;
using Amazon.DynamoDBv2.DataModel;
using FluentValidation;

namespace FTS.Precatorio.Domain.Trade
{
    [DynamoDBTable("test_trade")]
    public class Trade
    {
        [DynamoDBHashKey]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid GroupId { get; set; }
        public DateTime DataInc { get; set; }
        public string UsuInc { get; set; }

        public Trade() { }

        public class Factory
        {
            public Trade Create(string code, Guid groupId)
            {
                var trade = new Trade
                {
                    Code = code,
                    GroupId = groupId
                };

                return trade;
            }
        }
    }

    public class CreateTradeValidator : AbstractValidator<Trade>
    {
        public CreateTradeValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Field Code is required.");
            RuleFor(x => x.UsuInc).NotEmpty().WithMessage("Field UsuInc is required.");
        }
    }
}