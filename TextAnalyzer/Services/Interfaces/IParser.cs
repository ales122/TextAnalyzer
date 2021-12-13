using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TextAnalyzer.Models.Interfaces;

namespace TextAnalyzer.Services.Interfaces
{
    public interface IParser
    {
        public IText ParseText(StreamReader reader);
        public IText ParseText(ICollection<string> strings);

    }
}
