using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations
{
    public class DeletIdRequestValidator : AbstractValidator<DeleteIdAvisosRequest>
    {
        public DeletIdRequestValidator()
        {
            RuleFor(c => c.DeletId).GreaterThan(0);
        }
    }
}
