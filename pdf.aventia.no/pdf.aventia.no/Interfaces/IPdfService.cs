using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using pdf.aventia.no.Models.Entities;

// Define the namespace for the interfaces
namespace pdf.aventia.no.Interfaces
{
    // Define the IPdfService interface
    public interface IPdfService
    {
        // Asynchronously index a specific PDF by its id
        Task IndexPdf(int pdfId, CancellationToken cancellationToken = default);
        
        // Asynchronously index all PDF files in a specific folder
        Task IndexAllPdfFilesInFolder(string folderPath = pdf.aventia.no.GlobalSettings.DefaultFolderPath, 
            CancellationToken cancellationToken = default);
        
        // Asynchronously search for a specific word in all indexed PDFs
        Task<IEnumerable<string>> SearchPdfsAsync(string word, CancellationToken cancellationToken = default);
        
        // Process all PDF files, implementation depends on the business requirements
        Task ProcessPdfFiles(CancellationToken cancellationToken = default);
    }
}