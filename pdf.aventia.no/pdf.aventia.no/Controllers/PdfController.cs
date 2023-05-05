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
            string folderPath = @"C:\Users\amund\OneDrive\Skrivebord\PdfTest";
            await pdfService.IndexAllPdfFilesInFolder(folderPath);
            return Ok("PDF files indexed successfully.");
        }

        // Get paragraphs for a specific PDF ID
        [HttpGet("{pdfId}")]
        public async Task<IActionResult> GetParagraphsByPdfId(int pdfId)
        {
            var paragraphs = await pdfService.JustASampleCall(pdfId);
            return Ok(paragraphs);
        }
    }
}