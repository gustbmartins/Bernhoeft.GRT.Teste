using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations
{
    public class GetIdAvisosRequestValidator : AbstractValidator<GetIdAvisosRequest>
    {
        public GetIdAvisosRequestValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0);
        }
    }
}
