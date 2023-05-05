namespace pdf.aventia.no.Database;
using Microsoft.EntityFrameworkCore;
using pdf.aventia.no.Models.Entities;

    public class PdfDbContext : DbContext
    {
        public PdfDbContext(DbContextOptions<PdfDbContext> options) : base(options)
        {
         
        }

        public virtual DbSet<Pdf> Pdfs { get; set; } = null!;
        public virtual DbSet<Paragraph> Paragraphs { get; set; } = null!;
        public virtual DbSet<Word> Words { get; set; } = null!;
    }
