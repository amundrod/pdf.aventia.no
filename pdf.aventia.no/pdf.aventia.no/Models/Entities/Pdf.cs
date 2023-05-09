using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace pdf.aventia.no.Models.Entities
{
    public class Pdf
    {
        public int id { get; set; }
        public string filepath { get; set; }
        public string text { get; set; } // Add this line to include the whole text

    }
}