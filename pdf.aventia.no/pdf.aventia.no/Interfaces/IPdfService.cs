using pdf.aventia.no.Models.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace pdf.aventia.no.Interfaces
{
    public interface IPdfService
    {
        Task IndexPdf(int pdfId, CancellationToken cancellationToken = default);
        Task IndexAllPdfFilesInFolder(string folderPath = @"C:\Users\amund\OneDrive\Skrivebord\PdfTest", CancellationToken cancellationToken = default);
        Task IndexSinglePdfFile(string folderPath = @"C:\Users\amund\OneDrive\Skrivebord\PdfTest", CancellationToken cancellationToken = default, int pdfid = 0);
        Task ProcessPdfFiles(CancellationToken cancellationToken = default);
        Task<List<Pdf>> SearchPdfsAsync(string word);
        Task SearchPdf(int pdfId, string word, CancellationToken cancellationToken = default);
    }
}