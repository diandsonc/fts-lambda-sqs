using System;
using System.ComponentModel.DataAnnotations;
using Amazon.DynamoDBv2.DataModel;
using FluentValidation;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace FTS.Precatorio.Domain.Trade
{
    [DynamoDBTable("test_trade")]
    public class Trade : AbstractValidator<Trade>
    {
        [DynamoDBHashKey]
        public Guid Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public Guid GroupId { get; set; }

        public DateTime DataInc { get; set; }

        [Required]
        [MaxLength(50), StringLength(50)]
        public string UsuInc { get; set; }

        [DynamoDBIgnore]
        public ValidationResult ValidationResult { get; protected set; }

        [DynamoDBIgnore]
        public new CascadeMode CascadeMode { get; set; }

        public Trade()
        {
            ValidationResult = new ValidationResult();
        }

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