using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.Core.Interfaces.Results;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1
{
    public class CreateAvisosRequest : IRequest<IOperationResult<object>>
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
            Ativo = Ativo,
            Titulo = Titulo,
            Mensagem = Mensagem,
            DataCriacao = DataCriacao
        };  
    }
}
