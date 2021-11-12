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
            BreakWord(@"C:\Users\Yurii Shymko\OneDrive\Рабочий стол\worldsForCheck.txt", @"C:\Users\Yurii Shymko\OneDrive\Рабочий стол\BreakedWorld.txt");
        }

        private static void BreakWord(string FileWithWordToCheck, string FileToWriteBreakedWord = @"C:\Users\Yurii Shymko\OneDrive\Рабочий стол\BreakedWorld.txt")
        {
            string[] ArrWithNonBreakingWords = File.ReadAllLines(FileWithWordToCheck);
            for (int i = 0; i < ArrWithNonBreakingWords.Length; i++)
            {
                StringBuilder sb = new StringBuilder();

                string item = ArrWithNonBreakingWords[i].ToLower();
                int length = 0; ;
                while (length < ArrWithNonBreakingWords[i].Length)
                {
                    if (dictionaryWithSimpleWorld.ContainsValue(item))
                    {
                        sb.Append(item + ",");
                        length += item.Length;
                        item = ArrWithNonBreakingWords[i].ToLower().Substring(item.Length);
                       
                    }
                    else
                    {
                        if (item.Length > 0)
                        {
                            item = item.Substring(0, item.Length - 1);
                        }
                        else
                        {
                            sb.Append(ArrWithNonBreakingWords[i].ToLower());
                            break;
                        }
                       
                    }
                }
                File.AppendAllText(FileToWriteBreakedWord, $"(in){ArrWithNonBreakingWords[i]} -> (out){sb.ToString().TrimEnd(new char[] { ',' })}" + "\n");
            }
        }

        private static void FillingInTheDictionary()
        {
            int a = 0; //позбутися змінної
            dictionaryWithSimpleWorld = File.ReadAllLines(@"C:\Users\Yurii Shymko\source\Repos\FoundWorldInside\FoundWorldInside\GermanWorld.txt") //зробити шлях універсальним
            .Select(x => x)
            .ToDictionary(y => a++, x => x.ToLower());
        }

    }
}
