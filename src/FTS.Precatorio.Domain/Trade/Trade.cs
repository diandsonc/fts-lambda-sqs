using System;
using Amazon.DynamoDBv2.DataModel;
using FluentValidation;
using FTS.Precatorio.Domain.Core;

namespace FTS.Precatorio.Domain.Trade
{
    [DynamoDBTable("test_trade")]
    public class Trade : Entity<Trade>
    {
        public string Code { get; private set; }
        public Guid GroupId { get; private set; }

        public Trade() { }

        public Trade(string code) : this()
        {
            Code = code;
        }

        public bool IsValidEdit()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Field is required.");

            return ValidationResult.IsValid;
        }

        public class Factory
        {
            public Trade Create(string code, Guid groupId)
            {
                var trade = new Trade(code)
                {
                    GroupId = groupId
                };

                return trade;
            }
        }
    }
}