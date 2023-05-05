using pdf.aventia.no.Models.Entities;

namespace pdf.aventia.no.Interfaces
{
    public interface IPdfService
    {
        Task IndexPdf(int pdfId, CancellationToken cancellationToken = default);
        Task<IEnumerable<string>> JustASampleCall(int pdfId, CancellationToken cancellationToken = default);
        Task ProcessPdfFiles(CancellationToken cancellationToken = default);
        Task<List<Pdf>> SearchPdfsAsync(string word);
    }
}