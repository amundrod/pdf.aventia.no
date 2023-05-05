namespace pdf.aventia.no.Models.Entities
{
    public class Paragraph
    {
        public int Id { get; set; }
        public int PdfId { get; set; }
        public string Text { get; set; } = string.Empty;
        public Pdf Pdf { get; set; }
        public ICollection<Word> Words { get; set; }

        public Paragraph()
        {
            Words = new List<Word>();
        }
    }
}