using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1
{
    public class PostAvisosHandler : IRequestHandler<CreateAvisosRequest, IOperationResult<object>>
    {
        private readonly IServiceProvider _serviceProvider;
        private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();
        public PostAvisosHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<IOperationResult<object>> Handle(CreateAvisosRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateAvisosRequestValidator().Validate(request);

                if (!validator.IsValid)
                    return OperationResult<object>.ReturnBadRequest().AddMessage(validator.Errors.Select(s => s.ErrorMessage));

                var criarAvisoRequest = request.ToEntity();

                await _avisoRepository.CriarAvisoAsync(criarAvisoRequest);

                return OperationResult<object>.ReturnCreated();

            }
            catch (Exception ex)
            {

                return OperationResult<object>.ReturnInternalServerError().AddMessage(ex.Message);
            }
        }
    }
}
