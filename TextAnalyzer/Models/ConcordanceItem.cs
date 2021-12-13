using System;
using System.Collections.Generic;
using System.Text;
using TextAnalyzer.Models.Interfaces;

namespace TextAnalyzer.Models
{
    public class ConcordanceItem
    {
        public IWord Word { get; set; }
        public int Count { get; set; }

        public ConcordanceItem() { }

        public ConcordanceItem(IWord word, int count)
        {
            Word = word;
            Count = count;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Word);
            sb.Append(" --- ");
            sb.Append(Count);
            return sb.ToString();
        }
    }
}
