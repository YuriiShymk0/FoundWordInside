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
                string wordForCheck = ArrWithNonBreakingWords[i].ToLower().Trim();
                int a = 0;
                int b = 0;
                int c = 0;
                for (int j = 0; j < ArrWithNonBreakingWords[i].Length;)
                {
                    if (dictionaryWithSimpleWorld.ContainsValue(wordForCheck))
                    {
                        b = wordForCheck.Length;
                        sb.Append(wordForCheck + ',');
                        c++;
                        j += wordForCheck.Length;
                        wordForCheck = ArrWithNonBreakingWords[i].Substring(wordForCheck.Length + a);
                        a += b;
                    }
                    else
                    {
                        if (wordForCheck.Length > 0)
                        {
                            wordForCheck = wordForCheck.Substring(0, wordForCheck.Length - 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                string str = ArrWithNonBreakingWords[i].Substring(sb.ToString().Length-c);
                if (str != "" && str.Length >= 1 )
                {
                    File.AppendAllText(FileToWriteBreakedWord, $"(in){ArrWithNonBreakingWords[i]} -> (out){ArrWithNonBreakingWords[i]}" + "\n");
                }
                else
                {
                    File.AppendAllText(FileToWriteBreakedWord, $"(in){ArrWithNonBreakingWords[i]} -> (out){(sb.ToString() + str).TrimEnd(new char[] { ' ', ',' })}" + "\n");
                }
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

