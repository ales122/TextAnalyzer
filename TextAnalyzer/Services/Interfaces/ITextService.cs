using System;
using System.Collections.Generic;
using System.Text;
using TextAnalyzer.DTO;
using TextAnalyzer.Models;
using TextAnalyzer.Models.Interfaces;

namespace TextAnalyzer.Services.Interfaces
{
    public interface ITextService
    {
        public IEnumerable<IWord> GetInterrogativeSentencesWordsWithLength(ICollection<ISentence> sentences, int length);
        public void ReplaceWords(ISentence sentence, int length, string newWord);
        public ICollection<ISentence> SortSentences(ICollection<ISentence> sentences);
        public ICollection<ISentence> RemoveWordsStartsWithConsonants(ICollection<ISentence> sentences, int length);
        public IEnumerable<ConcordanceItem> Concordance(IText text);
        public IEnumerable<ConcordanceItemsDTO> ConcordanceMorphy(IEnumerable<ConcordanceItem> items);

    }
}
