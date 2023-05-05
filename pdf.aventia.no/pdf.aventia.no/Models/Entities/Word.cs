namespace pdf.aventia.no.Models.Entities
{
    public class Word
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public ICollection<Paragraph> Paragraphs { get; set; }
    }
}