using Microsoft.EntityFrameworkCore;
using pdf.aventia.no.Database;
using pdf.aventia.no.Interfaces;

namespace pdf.aventia.no.Services
{
    public class PdfService : IPdfService
    {
        private readonly PdfDbContext context;

        public PdfService(PdfDbContext context) 
        {
            this.context = context;
        }

        public async Task<IEnumerable<string>> JustASampleCall(int id, CancellationToken cancellationToken = default)
        {
            return await context.Pdfs.Where(x => x.Id == id)
                .Select(x => x.Text)
                .ToListAsync(cancellationToken);
        }

        public async Task ProcessPdfFiles(CancellationToken cancellationToken = default)
        {
            // Use this.context.Pdfs to access the Pdfs DbSet<Pdf> property.
            throw new NotImplementedException();
        }
    }
}
