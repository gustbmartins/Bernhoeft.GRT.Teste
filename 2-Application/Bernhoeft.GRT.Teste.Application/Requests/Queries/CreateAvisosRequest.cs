using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries
{
    public class CreateAvisosRequest : IRequest<IOperationResult<GetAvisosResponse>>
    {
        public CreateAvisosRequest(string titulo, string mensagem)
        {
            Titulo = titulo;
            Mensagem = mensagem;
        }

        public bool Ativo { get { return true; } }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataCriacao { get { return DateTime.UtcNow; } }

        public AvisoEntity ToEntity() => new AvisoEntity
        {
            Ativo = this.Ativo,
            Titulo = this.Titulo,
            Mensagem = this.Mensagem,
            DataCriacao = this.DataCriacao
        };  
    }
}
