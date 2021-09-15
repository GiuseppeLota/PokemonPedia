using FluentValidation;

namespace PokemonPedia.Application.Validators
{
    public class PokemonNameValidator : AbstractValidator<string>
    {
        public PokemonNameValidator()
        {
            RuleFor(x => x).NotEmpty().WithMessage("Pokemon name should not be empty");
        }
    }
}
