using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pdf.aventia.no.Interfaces;

// Define the namespace for the controller
namespace pdf.aventia.no.Controllers
{
    // Use ApiController attribute to enable the Web API specific features
    [ApiController]

    // Define the route for the API
    [Route("api/pdf")]
    public class PdfController : ControllerBase
    {
        // Define a private readonly variable for the service interface
        private readonly IPdfService pdfService;

        // Constructor for the controller class
        public PdfController(IPdfService pdfService)
        {
            // Initialize the service interface
            this.pdfService = pdfService;
        }

        // API endpoint to index all PDF files in the folder
        // HTTP GET method with "index" route
        [HttpGet("index")]
        public async Task<IActionResult> IndexAllPdfFiles()
        {
            // Define the folder path
            string folderPath = pdf.aventia.no.GlobalSettings.DefaultFolderPath;
            
            // Call the service to index all PDF files in the folder
            await pdfService.IndexAllPdfFilesInFolder(folderPath);
            
            // Return a success message
            return Ok("PDF files indexed successfully.");
        }

        // API endpoint to search PDFs for a specific word
        // HTTP GET method with "search" route
        [HttpGet("search")]
        public async Task<IEnumerable<string>> SearchPdfsAsync(string word)
        {
            // If the search word is null or empty, return an empty list
            if (string.IsNullOrEmpty(word))
            {
                return new List<string>();
            }

            // Search for PDFs containing the specified word
            // and return highlighted paragraphs
            return await pdfService.SearchPdfsAsync(word);
        }
    }     
}