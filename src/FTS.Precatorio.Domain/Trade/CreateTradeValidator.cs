using FluentValidation;

namespace FTS.Precatorio.Domain.Trade
{
    public class CreateTradeValidator : AbstractValidator<Trade>
    {
        public CreateTradeValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Field Code is required.");
            RuleFor(x => x.UsuInc).NotEmpty().WithMessage("Field UsuInc is required.");
        }
    }
}