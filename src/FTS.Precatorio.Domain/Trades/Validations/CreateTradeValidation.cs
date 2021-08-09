using FluentValidation;

namespace FTS.Precatorio.Domain.Trades.Validations
{
    public class CreateTradeValidation : AbstractValidator<Trade>
    {
        public CreateTradeValidation()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("trade.code.required");
        }
    }
}