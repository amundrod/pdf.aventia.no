using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace pdf.aventia.no.Interfaces
{
    public interface IPdfDbContext
    {
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
