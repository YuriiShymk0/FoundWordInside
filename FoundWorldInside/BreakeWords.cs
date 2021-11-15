using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FoundWordInside
{
    internal class BreakeWords
    {
        private Dictionary<char, List<string>> _simpleWords = new Dictionary<char, List<string>>();

        /// <summary>
        /// Specifies the paths to the files
        /// </summary>
        /// <param name="pathBreakingWords">Full path to the file to write result of breaking</param>
        /// <param name="pathWordsForCheck">Full path to the file with word to check</param>
        /// <param name="pathWordsForDictionary">Full path to the file to populate the dictionary</param>
        internal void Menu(out string pathBreakingWords, out string pathWordsForCheck, out string pathWordsForDictionary)
        {
            while (true)
            {
                Console.Clear();
                pathWordsForCheck = TrimmedLineFromConsole("Enter the full path to the file with word to check: ");

                if (File.Exists(pathWordsForCheck))
                {
                    break;
                }
                ErrorMessage($"File {pathWordsForCheck} is not Exists ");
            }
            pathWordsForDictionary = TrimmedLineFromConsole("Enter the full path to the file to populate the dictionary: ");
            pathBreakingWords = TrimmedLineFromConsole("Enter the full path to the file to write result of breaking: ");
        }

        /// <summary>
        /// Try breaks the term into simple words if possible, otherwise returns without changes
        /// </summary>
        /// <param name="FileWithWordToCheck">Full path to the file with word to check</param>
        /// <param name="FileToWriteBreakedWord">Full path to the file to write result of breaking</param>
        internal void BreakWords(string FileWithWordToCheck, string FileToWriteBreakedWord)
        {
            if (!File.Exists(FileToWriteBreakedWord))
            {
                FileToWriteBreakedWord = @" \..\..\..\..\BreakedWord.txt";
            }
            if (_simpleWords == null || _simpleWords.Count == 0)
            {
                FillingInTheDictionary("");
            }

            string[] ArrWithNonBreakingWords = File.ReadAllLines(FileWithWordToCheck);

            foreach (var nonBreakingWords in ArrWithNonBreakingWords)
            {
                int lengthFoundWords = 0;
                int countWords = 0;
                char forCheck = nonBreakingWords[0];
                string wordForCheck = nonBreakingWords.ToLower();
                int sizeWordForCheck = nonBreakingWords.Length;
                List<string> listWithKeyValue = _simpleWords[wordForCheck[0]];
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < sizeWordForCheck && wordForCheck.Length > 0;)
                {
                    //if the first letter of the word we are looking for is not the same as the
                    //letter of the word at the moment, then rewrite List
                    if (wordForCheck[0] != forCheck)
                    {
                        listWithKeyValue = _simpleWords[wordForCheck[0]];
                        forCheck = wordForCheck[0];
                    }
                    else
                    {
                        if (!listWithKeyValue.Contains(wordForCheck))
                        {
                            wordForCheck = wordForCheck.Substring(0, wordForCheck.Length - 1);
                        }
                        else
                        {
                            //fix the size of the word found
                            int wordLength = wordForCheck.Length;
                            sb.Append(wordForCheck + ',');
                            countWords++;
                            j += wordLength;
                            wordForCheck = nonBreakingWords.Substring(wordForCheck.Length + lengthFoundWords);
                            //add the size of the found word
                            lengthFoundWords += wordLength; 
                        }
                    }
                }

                // find everything that did not get in sb
                string str = nonBreakingWords[(sb.ToString().Length - countWords)..];
                //check the possibility of the word
                if (str.Length < 3 && sizeWordForCheck - countWords > 4)
                {
                    File.AppendAllText(FileToWriteBreakedWord, $"(in){nonBreakingWords} -> (out){(sb.ToString() + str).TrimEnd(new char[] { ' ', ',' })}" + "\n");
                }
                else
                {
                    File.AppendAllText(FileToWriteBreakedWord, $"(in){nonBreakingWords} -> (out){nonBreakingWords}" + "\n");
                }
            }
        }

        /// <summary>
        ///Fills the dictionary with words from the file if it exists otherwise fills with words from the default file
        /// </summary>
        /// <param name="parthTofile">Full path to the file to populate the dictionary</param>
        internal void FillingInTheDictionary(string parthTofile)
        {
            if (!File.Exists(parthTofile))
            {
                parthTofile = @" \..\..\..\..\GermanWords.txt";
            }
            _simpleWords = File.ReadAllLines(parthTofile)
                                           .Select(m => m.ToLower())
                                           .GroupBy(o => o[0])
                                           .ToDictionary(grp => grp.Key, grp => grp.ToList());
        }

        private void ErrorMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine("Try again. Press any key to continue...");
            Console.ReadKey();
        }

        private string TrimmedLineFromConsole(string message = "")
        {
            Console.Write(message);
            return Console.ReadLine().Trim(new char[] { ' ', '"', '\'' });
        }
    }
}
