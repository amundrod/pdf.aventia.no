namespace pdf.aventia.no.Interfaces
{
    public interface IPdfService
    {
        Task<IEnumerable<string>> JustASampleCall(int id, CancellationToken cancellationToken = default);
        Task ProcessPdfFiles(CancellationToken cancellationToken = default);
    }
}
