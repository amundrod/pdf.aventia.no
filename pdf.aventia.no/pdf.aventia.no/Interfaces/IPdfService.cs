using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using pdf.aventia.no.Models.Entities;

namespace pdf.aventia.no.Interfaces
{
    public interface IPdfService
    {
        Task IndexPdf(int pdfId, CancellationToken cancellationToken = default);
        Task IndexAllPdfFilesInFolder(string folderPath, CancellationToken cancellationToken = default);
        Task IndexSinglePdfFile(string folderPath, CancellationToken cancellationToken = default, int pdfid = 0);
        //Task<IEnumerable<string>> JustASampleCall(int pdfId, CancellationToken cancellationToken = default);
        Task ProcessPdfFiles(CancellationToken cancellationToken = default);
        Task<List<Pdf>> SearchPdfsAsync(string word);
    }
}