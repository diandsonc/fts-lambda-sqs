using FluentValidation;

namespace FTS.Precatorio.Domain.Trade.Validations
{
    public class CreateTradeValidation : AbstractValidator<Trade>
    {
        public CreateTradeValidation()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("trade.code.required");
        }
    }
}