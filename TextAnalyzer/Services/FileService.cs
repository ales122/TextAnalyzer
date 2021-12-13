﻿using System;
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
using TextAnalyzer.DTO;
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
                case "application/pdf": { return GetPDF(path); }
                //case "application/msword":
                case "text/plain": { return GetTXT(path); }
                //case "application/vnd.openxmlformats-officedocument.wordprocessingml.document": { return GetDOCX(path); }
                default: { return null; }
            }
        }

        private ICollection<string> GetDOC(string filename)
        {
            ICollection<string> lines = new List<string>();
            Package wordPackage = Package.Open(filename, FileMode.Open, FileAccess.Read);

            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(wordPackage))
            {
                using (StreamReader sr = new StreamReader(wordDocument.MainDocumentPart.GetStream()))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }

            wordPackage.Close();

            return lines;
        }

        private ICollection<string> GetPDF(string filename)
        {
            var pageText = new List<string>();

            using (PdfDocument pdfDocument = new PdfDocument(new PdfReader(filename)))
            {
                var pageNumbers = pdfDocument.GetNumberOfPages();
                for (int i = 1; i <= pageNumbers; i++)
                {
                    LocationTextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                    PdfCanvasProcessor parser = new PdfCanvasProcessor(strategy);
                    parser.ProcessPageContent(pdfDocument.GetFirstPage());
                    pageText.Add(strategy.GetResultantText());
                }
            }
            return pageText;
        }

        //private ICollection<string> GetDOCX(string filename)
        //{
        //    using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(filename, false))
        //    {
        //        var paragraphs = wordDocument.MainDocumentPart.RootElement.Descendants<Paragraph>();
        //        ICollection<string> paragraphsStr = new List<string>();
        //        foreach (var item in paragraphs)
        //        {
        //            paragraphsStr.Add(item.InnerText);
        //        }

        //        return paragraphsStr;
        //    }
        //}

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

        public void WriteData(IEnumerable<ConcordanceItemsDTO> items, string filename)
        {
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            string[] filepath = filename.Split('.');
            FileInfo file = new FileInfo(filepath[0] + ".xlsx");
            using (var excelPackage = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Лист 1");
                DataTable data = new DataTable();
                data.Columns.Add("Слово", typeof(string));
                data.Columns.Add("Количество", typeof(int));
                foreach (var item in items)
                {
                    data.Rows.Add(item.Words, item.Counter);
                }
                worksheet.Cells["A1"].LoadFromDataTable(data, true);
                excelPackage.Save();
            }
        }
    }
}
