using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalyzer.Models.Separators
{
    public class ClosingSeparators : Separator
    {
        string[] closingSeparators = { ")", "]", "}", "»", "”", "’" };

        public override string[] GetSeparators()
        {
            return closingSeparators;
        }
    }
}
