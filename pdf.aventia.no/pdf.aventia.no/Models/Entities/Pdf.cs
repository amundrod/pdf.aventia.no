namespace pdf.aventia.no.Models.Entities
{
    public class Pdf
    {
        public int Id { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public virtual ICollection<Paragraph> Paragraphs { get; set; } = new List<Paragraph>();
    }
}