using Microsoft.EntityFrameworkCore;
using pdf.aventia.no.Models.Entities;

// Define the namespace for the database context
namespace pdf.aventia.no.Database
{
    // Define the class for the PDF database context, inheriting from DbContext
    public class PdfDbContext : DbContext
    {
        // Define the constructor which receives DbContextOptions as input
        // The base keyword is used to call the base class constructor
        public PdfDbContext(DbContextOptions<PdfDbContext> options) : base(options)
        {
        }

        // Define a virtual DbSet property for the Pdf entities
        // The virtual keyword allows for the property to be overridden in derived classes
        // The DbSet represents a collection of all entities in the context, or that can be queried from the database, of a given type
        // The null! is used to suppress nullable reference type warnings, assuming that the property will be initialized before being used
        public virtual DbSet<Pdf> Pdfs { get; set; } = null!;
    }
}