using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Core.Extensions;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Queries;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1
{
    public class GetAvisosHandler : IRequestHandler<GetAvisosRequest, IOperationResult<IEnumerable<GetAvisosResponse>>>,
        IRequestHandler<GetIdAvisosRequest, IOperationResult<GetAvisosResponse>>  
    {
        private readonly IServiceProvider _serviceProvider;

        private IContext _context => _serviceProvider.GetRequiredService<IContext>();
        private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();

        public GetAvisosHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<IOperationResult<IEnumerable<GetAvisosResponse>>> Handle(GetAvisosRequest request, CancellationToken cancellationToken)
        {
            var result = await _avisoRepository.ObterTodosAvisosAsync(TrackingBehavior.NoTracking);
            if (!result.HaveAny())
                return OperationResult<IEnumerable<GetAvisosResponse>>.ReturnNoContent();

            return OperationResult<IEnumerable<GetAvisosResponse>>.ReturnOk(result.Select(x => (GetAvisosResponse)x));
        }

        public async Task<IOperationResult<GetAvisosResponse>> Handle(GetIdAvisosRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new GetIdAvisosRequestValidator().Validate(request);

                if(!validator.IsValid)
                    return OperationResult<GetAvisosResponse>.ReturnBadRequest().AddMessage(validator.Errors.Select(s => s.ErrorMessage));

                var result = await _avisoRepository.ObterAvisoPorIdAsync(request.Id);

                if (result is null)
                    return OperationResult<GetAvisosResponse>.ReturnNotFound();

                return OperationResult<GetAvisosResponse>.ReturnOk((GetAvisosResponse)result);
            }
            catch (Exception ex)
            {

                return OperationResult<GetAvisosResponse>.ReturnInternalServerError(ex);
            }

        }

       
    }
}