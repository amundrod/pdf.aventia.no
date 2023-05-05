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

        [Route("{pdfId}")]
        [HttpGet]
        public async Task Get(int pdfId)
        {
            string folderPath = @"C:\Users\elias\Downloads\PDF";
            await pdfService.IndexAllPdfFilesInFolder(folderPath);
        }
    }
}