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
    // The PdfService class implements the IPdfService interface
    public class PdfService : IPdfService
    {
        // Dependency injection of PdfDbContext
        private readonly PdfDbContext context;

        public PdfService(PdfDbContext context)
        {
            this.context = context;
        }

        // Indexes a PDF by ID
        public async Task IndexPdf(int pdfid, CancellationToken cancellationToken = default)
        {
            // Find PDF by ID
            var pdf = await context.Pdfs.FindAsync(pdfid);
            
            // Load the PDF document
            var pdfDoc = new IronPdf.PdfDocument(pdf.filepath);
            
            // Extract all text from the PDF
            var extractedText = pdfDoc.ExtractAllText();

            if (extractedText != null)
            {
                // Store the whole extracted text
                pdf.text = extractedText;

                // Split the extracted text into sentences
                var sentences = extractedText.Split(new string[] { ".", "!", "?" }, StringSplitOptions.RemoveEmptyEntries);

                // Save the changes to the database
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        // Indexes all PDF files in a folder
        public async Task IndexAllPdfFilesInFolder(string folderPath = pdf.aventia.no.GlobalSettings.DefaultFolderPath,
            CancellationToken cancellationToken = default)
        {
            // Get all PDF files in the folder
            var pdfFilePaths = Directory.GetFiles(folderPath, "*.pdf");

            // Loop through each file
            foreach (var pdfFilePath in pdfFilePaths)
            {
                // Create a new PDF record
                var pdf = new Pdf { filepath = pdfFilePath };
                
                // Add the new record to the database
                context.Pdfs.Add(pdf);
                await context.SaveChangesAsync(cancellationToken);
                
                // Index the new PDF
                await IndexPdf(pdf.id, cancellationToken);
            }
        }

        // Searches for a word in all PDFs
        public async Task<IEnumerable<string>> SearchPdfsAsync(string word, CancellationToken cancellationToken)
        {
            var results = new List<string>();

            // Check if word is null or empty
            if (string.IsNullOrEmpty(word))
            {
                return results;
            }

            // Get all PDFs
            var pdfs = await context.Pdfs.ToListAsync(cancellationToken);

            // Loop through each PDF
            foreach (var pdf in pdfs)
            {
                // Check if pdf.text is not null
                if (pdf.text != null)
                {
                    // Split the text into sentences
                    var sentences = pdf.text.Split(new[] { ".", "!", "?" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < sentences.Length; i++)
                    {
                        // Check if sentence contains the word
                        if (sentences[i].Contains(word))
                        {
                            // Highlight the word and return the sentence and the next two
                            var endIndex = Math.Min(i + 2, sentences.Length - 1); 
                            var excerpt = string.Join(". ", sentences.Skip(i).Take(endIndex - i + 1));
                            var highlightedExcerpt = excerpt.Replace(word, "*" + word + "*");
                            results.Add($"PDF ID: {pdf.id} - Excerpt: {highlightedExcerpt}");
                        }
                    }
                }
            }

            // Return the search results
            return results;
        }



        public async Task ProcessPdfFiles(CancellationToken cancellationToken = default)
        {
            // Implement the logic for processing PDF files here
            throw new NotImplementedException();
        }
    }

}