// Define the namespace for the models/entities
namespace pdf.aventia.no.Models.Entities
{
    // Define the Word class
    public class Word
    {
        // Property for the ID of the Word. This is typically the primary key.
        public int id { get; set; }

        // Property for the text of the Word.
        public string text { get; set; } = string.Empty;

        // Foreign key for the Pdf. This is used to link the Word to its parent Pdf.
        public int PdfId { get; set; }

        // Navigation property for the Pdf. This is used to navigate the relationship between 
        // the Word and its parent Pdf in an object-oriented way.
        public Pdf Pdf { get; set; }
    }
}