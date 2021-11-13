using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace FoundWordInside
{
    class MainClass
    {
        private static Dictionary<int, string> dictionaryWithSimpleWorld = new Dictionary<int, string>();

        private static void Main(string[] args)
        {
            FillingInTheDictionary();
            BreakWord(@"C:\Users\Yurii Shymko\OneDrive\Рабочий стол\wordsForCheck.txt", @"C:\Users\Yurii Shymko\OneDrive\Рабочий стол\BreakedWord.txt");
        }

        private static void BreakWord(string FileWithWordToCheck, string FileToWriteBreakedWord = @"C:\Users\Yurii Shymko\OneDrive\Рабочий стол\BreakedWord.txt")
        {
            string[] ArrWithNonBreakingWords = File.ReadAllLines(FileWithWordToCheck);
            for (int i = 0; i < ArrWithNonBreakingWords.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                string wordForCheck = ArrWithNonBreakingWords[i].ToLower();

                for (int j = 0; j < wordForCheck.Length;)
                {
                    if (dictionaryWithSimpleWorld.ContainsValue(wordForCheck))
                    {
                        sb.Append(wordForCheck + ',');
                        j += wordForCheck.Length;
                    }
                    else
                    {
                        wordForCheck = wordForCheck.Substring(0, wordForCheck.Length - 1);
                    }

                }
                string str = ArrWithNonBreakingWords[i].Trim(sb.ToString().ToCharArray());
                File.AppendAllText(FileToWriteBreakedWord, $"(in){ArrWithNonBreakingWords[i]} -> (out){(sb.ToString() + str).TrimEnd(new char[] {' ',','})}" + "\n");
            }
        }


        private static void FillingInTheDictionary()
        {
            int a = 0; //позбутися змінної
            dictionaryWithSimpleWorld = File.ReadAllLines(@"C:\Users\Yurii Shymko\source\Repos\FoundWorldInside\FoundWorldInside\GermanWord.txt") //зробити шлях універсальним
            .Select(x => x)
            .ToDictionary(y => a++, x => x.ToLower());
        }
    }
}

