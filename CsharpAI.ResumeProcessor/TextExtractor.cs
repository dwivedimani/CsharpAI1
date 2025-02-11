using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.ResumeProcessor.Interfaces;

namespace CsharpAI.ResumeProcessor
{
    public class TextExtractor : ITextExtractor
    {
        public async Task<string> ExtractText(string filePath)
        {
            // Mock implementation (Use Tesseract, PDFBox, or Azure OCR)
            return await File.ReadAllTextAsync(filePath);
        }
    }
}
