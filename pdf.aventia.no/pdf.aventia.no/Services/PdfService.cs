using Microsoft.EntityFrameworkCore;
using pdf.aventia.no.Database;
using pdf.aventia.no.Interfaces;
using pdf.aventia.no.Models.Entities;
using IronPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace pdf.aventia.no.Services
{
    public class PdfService : IPdfService
    {
        private readonly PdfDbContext context;

        public PdfService(PdfDbContext context) 
        {
            this.context = context;
        }

        public async Task IndexPdf(int pdfId, CancellationToken cancellationToken = default)
        {
            var pdf = await context.Pdfs.FindAsync(pdfId);
            var pdfDoc = new IronPdf.PdfDocument(pdf.FilePath);
            var extractedText = pdfDoc.ExtractAllText();
            var paragraphs = extractedText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var paragraphText in paragraphs)
            {
                var paragraph = new Paragraph { Text = paragraphText, Pdf = pdf };
                var words = paragraphText.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var wordText in words)
                {
                    var word = await context.Words.FirstOrDefaultAsync(w => w.Text == wordText);
                    if (word == null)
                    {
                        word = new Word { Text = wordText };
                        context.Words.Add(word);
                    }
                    paragraph.Words.Add(word);
                }
                context.Paragraphs.Add(paragraph);
            }
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task IndexAllPdfFilesInFolder(string folderPath = @"C:\Users\elias\Downloads\PDF", CancellationToken cancellationToken = default)
        {
            var pdfFilePaths = Directory.GetFiles(folderPath, "*.pdf");

            foreach (var pdfFilePath in pdfFilePaths)
            {
                var pdf = new Pdf { FilePath = pdfFilePath };
                context.Pdfs.Add(pdf);
                await context.SaveChangesAsync(cancellationToken);
                await IndexPdf(pdf.Id, cancellationToken);
            }
        }

        public async Task<IEnumerable<string>> JustASampleCall(int pdfId, CancellationToken cancellationToken = default)
        {
            return await context.Pdfs.Where(x => x.Id == pdfId)
                .SelectMany(x => x.Paragraphs)
                .Select(x => x.Text)
                .ToListAsync(cancellationToken);
        }

        public async Task ProcessPdfFiles(CancellationToken cancellationToken = default)
        {
            // Use this.context.Pdfs to access the Pdfs DbSet<Pdf> property.
            throw new NotImplementedException();
        }

        public async Task<List<Pdf>> SearchPdfsAsync(string word)
        {
            return await context.Words
                .Where(w => w.Text == word)
                .SelectMany(w => w.Paragraphs)
                .Select(p => p.Pdf)
                .Distinct()
                .ToListAsync();
        }
    }
}
