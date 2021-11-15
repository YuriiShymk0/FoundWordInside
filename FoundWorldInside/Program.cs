namespace FoundWordInside
{
    public class MainClass
    {
        public static void Main(string[] args)
        {   
            BreakeWords breakeWords = new BreakeWords();

            string pathBreakingWords, pathWordsForCheck, pathWordsForDictionary;

            if (args.Length < 2 )
            {
                breakeWords.Menu(out pathBreakingWords, out pathWordsForCheck, out pathWordsForDictionary);
            }
            else
            {
                pathBreakingWords = GetTrimmed(args[0]);
                pathWordsForCheck = GetTrimmed(args[1]);
                pathWordsForDictionary = GetTrimmed(args[2]);
            }
            
            breakeWords.FillingInTheDictionary(pathWordsForDictionary);
            breakeWords.BreakWords(pathWordsForCheck,pathBreakingWords);
        }

        private static string GetTrimmed(string strForTrim)
        {
            return strForTrim.Trim(new char[] { ' ', '"', '\'' });
        }
    }
}

