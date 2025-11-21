using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Attributes;
using Bernhoeft.GRT.Core.EntityFramework.Infra;
using Bernhoeft.GRT.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Bernhoeft.GRT.ContractWeb.Infra.Persistence.SqlServer.ContractStore.Repositories
{
    [InjectService(Interface: typeof(IAvisoRepository))]
    public class AvisoRepository : Repository<AvisoEntity>, IAvisoRepository
    {
        public AvisoRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<AvisoEntity> AtualizarAvisoAsync(int id, string mensagem, TrackingBehavior tracking = TrackingBehavior.Default, CancellationToken cancellationToken = default)
        {
            var query = tracking is TrackingBehavior.NoTracking ? Set.AsNoTrackingWithIdentityResolution() : Set;
            var aviso = await query.Where(wh => wh.Id == id && wh.Ativo).SingleOrDefaultAsync();

            if (aviso?.Id is not null)
            {
                aviso.DataAlteracao = DateTime.Now;
                aviso.Mensagem = mensagem;

                Set.Update(aviso);
                await Context.SaveChangesAsync(cancellationToken);

            }
            return await query.Where(wh => wh.Id == id).SingleOrDefaultAsync();
            

        }

        public async Task CriarAvisoAsync(AvisoEntity avisoEntity, CancellationToken cancellationToken = default)
        {
            await Set.AddAsync(avisoEntity, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

        }

        public async Task<bool> DeletarAvisoAsync(int id, TrackingBehavior tracking = TrackingBehavior.Default, CancellationToken cancellationToken = default)
        {
            var query = tracking is TrackingBehavior.NoTracking ? Set.AsNoTrackingWithIdentityResolution() : Set;
            var aviso = await query.Where(wh => wh.Id == id && wh.Ativo).SingleOrDefaultAsync();

            if (aviso?.Id is not null)
            {
                aviso.Ativo = false;
                aviso.DataAlteracao = DateTime.Now;

                Set.Update(aviso);
                await Context.SaveChangesAsync(cancellationToken);

                return true;

            }

            return false;
        }

        public Task<AvisoEntity> ObterAvisoPorIdAsync(int id, TrackingBehavior tracking = TrackingBehavior.Default, CancellationToken cancellationToken = default)
        {
            var query = tracking is TrackingBehavior.NoTracking ? Set.AsNoTrackingWithIdentityResolution() : Set;
            return query.Where(wh => wh.Id == id && wh.Ativo).SingleOrDefaultAsync();
        }

        public Task<List<AvisoEntity>> ObterTodosAvisosAsync(TrackingBehavior tracking = TrackingBehavior.Default, CancellationToken cancellationToken = default)
        {
            var query = tracking is TrackingBehavior.NoTracking ? Set.AsNoTrackingWithIdentityResolution() : Set;
            return query.Where(wh => wh.Ativo).ToListAsync();
        }
    }
}