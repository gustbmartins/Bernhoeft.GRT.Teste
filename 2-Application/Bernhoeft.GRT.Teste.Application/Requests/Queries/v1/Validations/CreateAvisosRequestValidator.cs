using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations
{
    public class CreateAvisosRequestValidator : AbstractValidator<CreateAvisosRequest>
    {
        public CreateAvisosRequestValidator()
        {
            RuleFor(c => c.Mensagem).NotEmpty().NotNull();
            RuleFor(c => c.Titulo).NotEmpty().NotNull();
        }
    }
}
