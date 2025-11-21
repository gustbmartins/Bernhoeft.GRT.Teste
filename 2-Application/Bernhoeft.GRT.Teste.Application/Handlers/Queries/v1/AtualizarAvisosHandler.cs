using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1
{
    public class AtualizarAvisosHandler : IRequestHandler<AtualizarAvisosRequest, IOperationResult<GetAvisosResponse>>
    {
        private readonly IServiceProvider _serviceProvider;
        private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();
        public AtualizarAvisosHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<IOperationResult<GetAvisosResponse>> Handle(AtualizarAvisosRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new AtualizarAvisosResquestValidator().Validate(request);
                if (!validator.IsValid)
                    return OperationResult<GetAvisosResponse>.ReturnBadRequest().AddMessage(validator.Errors.Select(s => s.ErrorMessage));

                var result = await _avisoRepository.AtualizarAvisoAsync(request.Id, request.Mensagem);

                return result is null ? OperationResult<GetAvisosResponse>.ReturnNoContent() : OperationResult<GetAvisosResponse>.ReturnOk(result);

            }
            catch (Exception ex)
            {

                return OperationResult<GetAvisosResponse>.ReturnInternalServerError().AddMessage(ex.Message);
            }


        }
    }
}
