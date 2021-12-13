using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalyzer.Models.Interfaces
{
    public interface IWord : ISentenceItem
    {
        public int Count { get; }
        public string FirstChar { get; }
    }
}
