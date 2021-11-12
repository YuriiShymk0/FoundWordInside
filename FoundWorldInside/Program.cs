using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace FoundWorldInside
{
    class MainClass
    {
        private static Dictionary<int, string> dictionaryWithSimpleWorld = new Dictionary<int, string>();

        private static void Main(string[] args)
        {
             FillingInTheDictionary();
            BreakWorld(@"C:\Users\Yurii Shymko\OneDrive\Рабочий стол\worldsForCheck.txt", @"C:\Users\Yurii Shymko\OneDrive\Рабочий стол\BreakedWorld.txt");
        }

        private static void BreakWorld(string FileWithWorldToCheck, string FileToWriteBreakedWorld = @"C:\Users\Yurii Shymko\OneDrive\Рабочий стол\BreakedWorld.txt")
        {
            string[] ArrWithNonBreakingWorls = File.ReadAllLines(FileWithWorldToCheck);
            for (int i = 0; i < ArrWithNonBreakingWorls.Length; i++)
            {
                StringBuilder sb = new StringBuilder();

                string item = ArrWithNonBreakingWorls[i].ToLower();
                int length = 0; ;
                while (length < ArrWithNonBreakingWorls[i].Length)
                {
                    if (dictionaryWithSimpleWorld.ContainsValue(item))
                    {
                        sb.Append(item + ",");
                        length += item.Length;
                        item = ArrWithNonBreakingWorls[i].ToLower().Substring(item.Length);
                       
                    }
                    else
                    {
                        if (item.Length > 0)
                        {
                            item = item.Substring(0, item.Length - 1);
                        }
                        else
                        {
                            sb.Append(ArrWithNonBreakingWorls[i].ToLower());
                            break;
                        }
                       
                    }
                }
                File.AppendAllText(FileToWriteBreakedWorld, $"(in){ArrWithNonBreakingWorls[i]} -> (out){sb.ToString().TrimEnd(new char[] { ',' })}" + "\n");
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
