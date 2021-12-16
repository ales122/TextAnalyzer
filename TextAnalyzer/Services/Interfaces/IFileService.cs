using System;
using System.Collections.Generic;
using System.Text;
using TextAnalyzer.Models.Interfaces;

namespace TextAnalyzer.Services.Interfaces
{
    public interface IFileService
    {
        public ICollection<string> GetData(string path, string contentType);
        public void Write(IText text, string filename);
    }
}
