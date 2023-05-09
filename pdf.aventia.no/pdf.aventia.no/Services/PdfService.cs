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
            var pdfDoc = new IronPdf.PdfDocument(pdf.filepath);
            var extractedText = pdfDoc.ExtractAllText();

            // Set the Text property of the Pdf object
            pdf.text = extractedText;
            // Save the changes to the database
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task IndexAllPdfFilesInFolder(string folderPath = pdf.aventia.no.GlobalSettings.DefaultFolderPath,
            CancellationToken cancellationToken = default)
        {
            var pdfFilePaths = Directory.GetFiles(folderPath, "*.pdf");

            foreach (var pdfFilePath in pdfFilePaths)
            {
                var pdf = new Pdf { filepath = pdfFilePath };
                context.Pdfs.Add(pdf);
                await context.SaveChangesAsync(cancellationToken);
                await IndexPdf(pdf.id, cancellationToken);
            }
        }

        public async Task IndexSinglePdfFile(string pdfid,
            string folderPath = pdf.aventia.no.GlobalSettings.DefaultFolderPath,
            CancellationToken cancellationToken = default)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(folderPath, pdfid + ".pdf");
            string filePath = files.FirstOrDefault();

            if (filePath != null)
            {
                var pdf = new Pdf { filepath = filePath };
                context.Pdfs.Add(pdf);
                await context.SaveChangesAsync(cancellationToken);
                await IndexPdf(pdf.id, cancellationToken);
            }
            else
            {
                // Handle the case where the file was not found
            }
        }

        public async Task<IEnumerable<Pdf>> SearchPdfsAsync(string word, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(word))
            {
                return new List<Pdf>();
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "The context object is null.");
            }

            Console.Write(word);
            // Search for PDFs containing the specified word
            return await context.Pdfs
                .Where(p => EF.Functions.Like(p.text, $"%{word}%"))
                .ToListAsync(cancellationToken);
        }



        public async Task ProcessPdfFiles(CancellationToken cancellationToken = default)
        {
            // Implement the logic for processing PDF files here
            throw new NotImplementedException();
        }

       /* Task IPdfService.SearchPdfsAsync(string word, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        } */
    }
}
