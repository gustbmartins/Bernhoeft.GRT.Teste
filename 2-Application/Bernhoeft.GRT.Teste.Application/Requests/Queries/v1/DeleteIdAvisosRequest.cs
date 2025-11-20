using Bernhoeft.GRT.Core.Interfaces.Results;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1
{
    public class DeleteIdAvisosRequest : IRequest<IOperationResult<object>>
    {
        public int DeletId { get; set; }
    }
}
