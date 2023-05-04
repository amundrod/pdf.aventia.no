using Microsoft.EntityFrameworkCore;
using pdf.aventia.no.Models.Entities;

namespace pdf.aventia.no.Database
{
    public class PdfDbContext : DbContext
    {
        public PdfDbContext(DbContextOptions<PdfDbContext> options) : base(options)
        {
         
        }

        public virtual DbSet<Pdf> Pdfs { get; set; } = null!;

    }
}
