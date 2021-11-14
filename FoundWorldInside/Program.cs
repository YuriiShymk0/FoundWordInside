using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace FoundWordInside
{
    static class MainClass
    {
        private static Dictionary<char, List<string>> dictionaryWithSimpleWord = new Dictionary<char, List<string>>();

        private static void Main(string[] args)
        {
            Menu(out string roadTofileWithBreakingWords, out string roadTofileWithWordsForCheck, out string roadTofileWithWordsForDictionary);
            if (File.Exists(roadTofileWithWordsForDictionary))
            {
                FillingInTheDictionary(roadTofileWithWordsForDictionary);
            }
            else
            {
                FillingInTheDictionary();
            }

            if (File.Exists(roadTofileWithBreakingWords))
            {
                BreakWords(roadTofileWithWordsForCheck, roadTofileWithBreakingWords);
            }
            else
                BreakWords(roadTofileWithWordsForCheck);
        }

        private static void Menu(out string roadTofileWithBreakingWords, out string roadTofileWithWordsForCheck, out string roadTofileWithWordsForDictionary)
        {
            while (true)
            {
                Console.Clear();

                Console.Write("Enter the full path to the file to populate the dictionary: ");
                roadTofileWithWordsForDictionary = Console.ReadLine().Trim(new char[] { ' ', '"', '\'' });

                Console.Write("Enter the full path to the file to write result of breaking: ");
                roadTofileWithBreakingWords = Console.ReadLine().Trim(new char[] { ' ', '"', '\'' });

                Console.Write("Enter the full path to the file with word to check: ");
                roadTofileWithWordsForCheck = Console.ReadLine().Trim(new char[] { ' ', '"', '\'' });
                if (File.Exists(roadTofileWithWordsForCheck))
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"File {roadTofileWithWordsForCheck} is not Exists ");
                    Console.WriteLine("Try again");
                    Thread.Sleep(1500);
                }
            }
        }

        private static void BreakWords(string FileWithWordToCheck, string FileToWriteBreakedWord = @" \..\..\..\BreakedWord.txt")
        {
            string[] ArrWithNonBreakingWords = File.ReadAllLines(FileWithWordToCheck);

            for (int i = 0; i < ArrWithNonBreakingWords.Length; i++)
            {
                var (a, b, c) = (0, 0, 0);
                char forcheck = ArrWithNonBreakingWords[i][0];
                string wordForCheck = ArrWithNonBreakingWords[i].ToLower();
                int sizeWordForCheck = ArrWithNonBreakingWords[i].Length;
                List<string> listWithKeyValue = dictionaryWithSimpleWord[wordForCheck[0]];
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < sizeWordForCheck && wordForCheck.Length > 0;)
                {
                    if (wordForCheck[0] != forcheck)
                    {
                        listWithKeyValue = dictionaryWithSimpleWord[wordForCheck[0]];
                        forcheck = wordForCheck[0];
                    }
                    else
                    {
                        if (!listWithKeyValue.Contains(wordForCheck))
                        {
                            wordForCheck = wordForCheck.Substring(0, wordForCheck.Length - 1);
                        }
                        else
                        {
                            b = wordForCheck.Length;
                            sb.Append(wordForCheck + ',');
                            c++;
                            j += wordForCheck.Length;
                            wordForCheck = ArrWithNonBreakingWords[i].Substring(wordForCheck.Length + a);
                            a += b;
                        }
                    }
                }

                string str = ArrWithNonBreakingWords[i][(sb.ToString().Length - c)..];
                if (str.Length < 3 && sizeWordForCheck - c > 4)
                {
                    File.AppendAllText(FileToWriteBreakedWord, $"(in){ArrWithNonBreakingWords[i]} -> (out){(sb.ToString() + str).TrimEnd(new char[] { ' ', ',' })}" + "\n");
                }
                else
                {
                    File.AppendAllText(FileToWriteBreakedWord, $"(in){ArrWithNonBreakingWords[i]} -> (out){ArrWithNonBreakingWords[i]}" + "\n");
                }
            }
        }

        private static void FillingInTheDictionary(string parthTofile = @" \..\..\..\..\GermanWords.txt")
        {
            dictionaryWithSimpleWord = File.ReadAllLines(parthTofile)
            .Select(m => m.ToLower())
            .GroupBy(o => o[0])
            .ToDictionary(grp => grp.Key, grp => grp.ToList());
        }
    }
}

