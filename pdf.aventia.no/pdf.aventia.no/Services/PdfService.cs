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

            }
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task IndexAllPdfFilesInFolder(string folderPath = pdf.aventia.no.GlobalSettings.DefaultFolderPath, CancellationToken cancellationToken = default)
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
        
        public Task IndexSinglePdfFile(string folderPath = GlobalSettings.DefaultFolderPath,
            CancellationToken cancellationToken = default, int pdfid = 0)
        {
            throw new NotImplementedException();
        }

        public async Task IndexSinglePdfFile(string pdfid, string folderPath = pdf.aventia.no.GlobalSettings.DefaultFolderPath, CancellationToken cancellationToken = default)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(folderPath, pdfid + ".pdf");
            string filePath = files.FirstOrDefault();

            if (filePath != null)
            {
                var pdf = new Pdf { FilePath = filePath };
                context.Pdfs.Add(pdf);
                await context.SaveChangesAsync(cancellationToken);
                await IndexPdf(pdf.Id, cancellationToken);
            }
            else
            {
                // Handle the case where the file was not found
            }
        }

        //public async Task<IEnumerable<string>> JustASampleCall(int pdfId, CancellationToken cancellationToken = default)
        //{
        //    return await context.Pdfs.Where(x => x.Id == pdfId)
        //        .SelectMany(x => x.Paragraphs)
        //        .Select(x => x.Text)
        //        .ToListAsync(cancellationToken);
        //}

        public async Task ProcessPdfFiles(CancellationToken cancellationToken = default)
        {
            // Use this.context.Pdfs to access the Pdfs DbSet<Pdf> property.
            throw new NotImplementedException();
        }

        public async Task<List<Pdf>> SearchPdfsAsync(string word)
        {
            throw new NotImplementedException();
        }

        public async Task SearchPdf(int pdfId, string word, CancellationToken cancellationToken = default)
        {
            var pdfs = await context.Pdfs
                .Where(x => x.Id == pdfId && x.Text.Contains(word))
                .ToListAsync(cancellationToken);
        }
    }
}