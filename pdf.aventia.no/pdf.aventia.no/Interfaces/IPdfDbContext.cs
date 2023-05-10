using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;
using System.Threading;

// Define the namespace for the interfaces
namespace pdf.aventia.no.Interfaces
{
    // Define the IPdfDbContext interface
    public interface IPdfDbContext
    {
        // The Entry method provides access to change tracking information and operations for a given entity
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        
        // The SaveChangesAsync method asynchronously saves all changes made in this context to the database.
        // It returns a Task that represents the asynchronous operation. 
        // The task result contains the number of objects written to the underlying database.
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}