using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pdf.aventia.no.Database;
using pdf.aventia.no.Interfaces;
using pdf.aventia.no.Models.Entities;
using pdf.aventia.no.Services;
using System.Collections.Generic;
using System.Threading;

namespace pdf.aventia.no.Controllers
{
    [ApiController]
    [Route("api/pdf")]
    public class PdfController : ControllerBase
    {
        private readonly IPdfService pdfService;
        private readonly PdfDbContext context;

        public PdfController(IPdfService pdfService, PdfDbContext context)
        {
            this.pdfService = pdfService;
            this.context = context;
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
        public async Task<IEnumerable<Pdf>> SearchPdfsAsync(string word, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(word))
            {
                return new List<Pdf>();
            }

            // Search for PDFs containing the specified word using the pdfService
            return await pdfService.SearchPdfsAsync(word, cancellationToken);
        }
    }     
}