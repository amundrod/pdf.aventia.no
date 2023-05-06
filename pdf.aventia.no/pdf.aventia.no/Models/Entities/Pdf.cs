namespace pdf.aventia.no.Models.Entities
{
    public class Pdf
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string Text { get; set; }
        public byte[] FileData { get; set; }
    }
}
