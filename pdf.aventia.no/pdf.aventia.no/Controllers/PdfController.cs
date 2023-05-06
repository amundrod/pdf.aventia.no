using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pdf.aventia.no.Interfaces;
using pdf.aventia.no.Models.Entities;
using pdf.aventia.no.Services;

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

        // Index a single PDF file by its ID
        [HttpGet("index/{pdfId}")]
        public async Task<IActionResult> IndexSinglePdfFile(int pdfId)
        {
            string folderPath = @"C:\Users\amund\OneDrive\Skrivebord\PdfTest";
            await pdfService.IndexSinglePdfFile(folderPath, default, pdfId);
            return Ok("PDF file indexed successfully.");
        }
    }
}