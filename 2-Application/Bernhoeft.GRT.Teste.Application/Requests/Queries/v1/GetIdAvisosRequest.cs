using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1
{
    public class GetIdAvisosRequest : IRequest<IOperationResult<GetAvisosResponse>>
    {
        public int Id { get; set; }
    }
}
