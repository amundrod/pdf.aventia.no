using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pdf.aventia.no.Interfaces;

namespace pdf.aventia.no.Controllers
{
    [ApiController]
    [Route("api/pdf")]
    public class PdfController : ControllerBase
    {
        private readonly IPdfService pdfService;

        public PdfController(IPdfService pdfService)
        {
            this.pdfService = pdfService;
        }

        // Index all PDF files in the folder
        [HttpGet("index")]
        public async Task<IActionResult> IndexAllPdfFiles()
        {
            string folderPath = pdf.aventia.no.GlobalSettings.DefaultFolderPath;
            await pdfService.IndexAllPdfFilesInFolder(folderPath);
            return Ok("PDF files indexed successfully.");
        }

        [HttpGet("search")]
        public async Task<IEnumerable<string>> SearchPdfsAsync(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return new List<string>();
            }

            // Search for PDFs containing the specified word and return highlighted paragraphs
            return await pdfService.SearchPdfsAsync(word);
        }
    }     
}