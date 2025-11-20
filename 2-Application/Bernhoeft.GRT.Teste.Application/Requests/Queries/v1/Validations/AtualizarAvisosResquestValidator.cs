using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations
{
    public class AtualizarAvisosResquestValidator : AbstractValidator<AtualizarAvisosRequest>
    {
        public AtualizarAvisosResquestValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0);
            RuleFor(c => c.Mensagem).NotNull().NotEmpty();
        }
    }
}
