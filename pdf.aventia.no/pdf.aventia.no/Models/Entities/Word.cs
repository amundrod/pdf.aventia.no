namespace pdf.aventia.no.Models.Entities
{
    public class Word
    {
        public int id { get; set; }
        public string text { get; set; } = string.Empty;

        // Foreign key for Pdf
        public int PdfId { get; set; }

        // Navigation property for Pdf
        public Pdf Pdf { get; set; }
    }
}