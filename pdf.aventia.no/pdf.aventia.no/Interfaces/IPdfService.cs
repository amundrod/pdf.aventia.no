using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using pdf.aventia.no.Models.Entities;

namespace pdf.aventia.no.Interfaces
{
    public interface IPdfService
    {
        Task IndexPdf(int pdfId, CancellationToken cancellationToken = default);
        Task IndexAllPdfFilesInFolder(string folderPath = pdf.aventia.no.GlobalSettings.DefaultFolderPath, CancellationToken cancellationToken = default);
        Task<IEnumerable<Pdf>> SearchPdfsAsync(string word, CancellationToken cancellationToken = default);

        Task ProcessPdfFiles(CancellationToken cancellationToken = default);
        
       
    }
}