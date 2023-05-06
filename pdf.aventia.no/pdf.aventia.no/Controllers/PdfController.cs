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
            string folderPath = pdf.aventia.no.GlobalSettings.DefaultFolderPath;
            await pdfService.IndexAllPdfFilesInFolder(folderPath);
            return Ok("PDF files indexed successfully.");
        }
        [HttpGet("search")]
        public async Task<List<Pdf>> SearchPdfsAsync(string word, CancellationToken cancellationToken = default)
        {
            return await context.Pdfs
                .Where(p => EF.Functions.Like(p.Text, $"%{word}%"))
                .ToListAsync(cancellationToken);
        }
        }
       
    }
