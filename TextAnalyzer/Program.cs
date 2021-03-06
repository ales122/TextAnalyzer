using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DeepMorphy;
using TextAnalyzer.Models;
using TextAnalyzer.Models.Interfaces;
using TextAnalyzer.Services;
using TextAnalyzer.Services.Interfaces;

namespace TextAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = ConfigurationManager.AppSettings;

            IParser parser = new Parser();
            IText text = new Text();
            IFileService fileService = new FileService();
            ITextService textService = new TextService();
            int choose;
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("1-Get text\n" +
                    "2-get words in interrogative sentences of given length\n" +
                    "3-sort sentences\n" +
                    "4-replace words\n" +
                    "5-remove words start with consonant of given length\n" +
                    "0-exit");
                choose = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (choose)
                {
                    case 1:
                        {
                      
                            ICollection<string> s = fileService.GetData(app["text"], "text/plain");
                            text = parser.ParseText(s);
                            foreach (var item in text.Sentences)
                            {
                                Console.WriteLine(item);
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Enter word length ");
                            int length = int.Parse(Console.ReadLine());
                            var temp = textService.GetInterrogativeSentencesWordsWithLength(text.Sentences, length);
                            if (temp != null)
                            {
                                foreach (var item in temp)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Not found");
                            }
                            break;
                        }
                    case 3:
                        {
                            text.Sentences = textService.SortSentences(text.Sentences);
                            Console.WriteLine(text);
                            break;
                        }
                    case 4:
                        {
                            Console.Write("Enter word length ");
                            int length = int.Parse(Console.ReadLine());
                            Console.Write("Enter word to replace ");
                            string newWord = Console.ReadLine();
                            foreach (var item in text.Sentences)
                                textService.ReplaceWords(item, length, newWord);
                            Console.WriteLine(text);
                            break;
                        }
                    case 5:
                        {
                            Console.Write("Enter word length ");
                            int length = int.Parse(Console.ReadLine());
                            text.Sentences = textService.RemoveWordsStartsWithConsonants(text.Sentences, length);
                            Console.WriteLine(text);
                            break;
                        }
                    case 0:
                        {
                            flag = false;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            fileService.Write(text, app["answer"]);
        }
    }
}
