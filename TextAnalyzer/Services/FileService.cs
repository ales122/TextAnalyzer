using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Packaging;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout.Element;
using OfficeOpenXml;
using TextAnalyzer.Models.Interfaces;
using TextAnalyzer.Services.Interfaces;
using LicenseContext = System.ComponentModel.LicenseContext;

namespace TextAnalyzer.Services
{
    public class FileService : IFileService
    {
        public ICollection<string> GetData(string path, string contentType)
        {
            switch (contentType)
            {
                case "text/plain": { return GetTXT(path); }
                default: { return null; }
            }
        }


        private ICollection<string> GetTXT(string filename)
        {
            ICollection<string> lines = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(filename, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return lines;
        }

        public void Write(IText text, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename, false))
            {
                foreach (var sentence in text.Sentences)
                {
                    writer.WriteLine(sentence);
                }
                writer.Close();
            }
        }


    }
}
