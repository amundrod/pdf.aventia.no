using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

// Define the namespace for the models/entities
namespace pdf.aventia.no.Models.Entities
{
    // Define the Pdf class
    public class Pdf
    {
        // Property for the ID of the PDF. This is usually the primary key.
        public int id { get; set; }

        // Property for the file path of the PDF.
        public string filepath { get; set; }

        // Property for the whole text of the PDF. This is typically extracted from the PDF file.
        public string text { get; set; }
    }
}