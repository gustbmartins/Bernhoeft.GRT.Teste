using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1
{
    public class DeleteAvisosHandler : IRequestHandler<DeleteIdAvisosRequest, IOperationResult<object>>
    {
        private readonly IServiceProvider _serviceProvider;
        private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();
        public DeleteAvisosHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<IOperationResult<object>> Handle(DeleteIdAvisosRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new DeletIdRequestValidator().Validate(request);
                if(!validator.IsValid)
                    return OperationResult<object>.ReturnBadRequest().AddMessage(validator.Errors.Select(s => s.ErrorMessage));

                var result = await _avisoRepository.DeletarAvisoAsync(request.DeletId);

                return result ? OperationResult<object>.ReturnOk("Aviso deletado com sucesso.") : OperationResult<object>.ReturnNoContent();
            }
            catch (Exception ex)
            {

                return OperationResult<object>.ReturnInternalServerError().AddMessage(ex.Message);
            }
        }
    }
}
