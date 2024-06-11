using System.Xml.Linq;

namespace MotFrequent
{
    internal class Program
    {
        static void Main(string[] args)
        {

            static void errorMessage(string message)
            {
                Console.WriteLine(message);
            }

            static bool existsInTheList(string name, List<WordsFound> list)
            {
                foreach (WordsFound word in list)
                {
                    if (word.getName() == name)
                    {
                        return true;
                    }
                }
                return false;
            }

            static string mostUsedWord(string paragraph, string[] bannedWords)
            {

                // Validate the length of the paragraph
                if (paragraph.Length < 1 || paragraph.Length > 1000)
                {
                    errorMessage("Le paragraphe doit contenir entre 1 et 1000 caractères.");
                    return null;
                }

                // Validate the length of the banned words
                if (bannedWords.Length < 0 || bannedWords.Length > 100)
                {
                    errorMessage("La liste de mots bannis doit contenir entre 0 et 100 mots.");
                    return null;
                }

                // Validate the number of banned words
                foreach (string word in bannedWords)
                {
                    if (word.Length < 1 || word.Length > 10)
                    {
                        errorMessage("Chaque mot de la liste de mots bannis doit contenir entre 1 et 10 caractères.");
                        return null;
                    }
                }

                // Transforms the uppercases to lowercases
                string loweredParagraph = paragraph.ToLower();
                string[] loweredBannedWords = new string[bannedWords.Length];
                for (int i = 0; i < bannedWords.Length; i++)
                {
                    loweredBannedWords[i] = bannedWords[i].ToLower();
                }

                // Verifies if the paragraph contains some unauthorized characters
                char[] acceptedChars = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', ' ', '.', ',', ';', '?', '!', 'é', 'è', 'ç', 'à' };
                for (int i = 0; i < loweredParagraph.Length; i++)
                {
                    bool isOk = false;
                    for (int j = 0; j < acceptedChars.Length; j++)
                    {
                        if (loweredParagraph[i] == acceptedChars[j])
                        {
                            isOk = true;
                            break;
                        }
                    }
                    if (!isOk)
                    {
                        errorMessage("Le paragraphe contient un caractère non-autorisé.");
                        return null;
                    }
                }

                // Ignores the special characters
                char[] charsToRemove = { ',', '.', ';', '?', '!' };
                foreach (char character in charsToRemove)
                {
                    loweredParagraph = loweredParagraph.Replace(character.ToString(), string.Empty);
                }

                // Transforms the paragraph to an array, then to a list
                string[] arrayedLoweredParagraph = loweredParagraph.Split(' ');
                List<string> listedLoweredParagraph = arrayedLoweredParagraph.ToList();

                // Removes banned words
                for (int i = 0; i < loweredBannedWords.Length; i++)
                {
                    for (int j = 0; j < listedLoweredParagraph.Count; j++)
                    {
                        if (loweredBannedWords[i] == listedLoweredParagraph[j])
                        {
                            listedLoweredParagraph.RemoveAt(j);
                            j--;
                        }
                    }
                }

                // Count the words
                List<WordsFound> wordsList = new List<WordsFound>();
                for (int i = 0; i < listedLoweredParagraph.Count; i++)
                {
                    if (!existsInTheList(listedLoweredParagraph[i], wordsList))
                    {
                        int currentCount = 0;
                        for (int k = 0; k < listedLoweredParagraph.Count; k++)
                        {
                            if (listedLoweredParagraph[i] == listedLoweredParagraph[k])
                            {
                                currentCount++;
                            }
                        }
                        wordsList.Add(new WordsFound(listedLoweredParagraph[i], currentCount));
                    }
                }

                // Keep the most used words
                List<WordsFound> mostUsedWords = new List<WordsFound>();
                int highestCount = 0;

                for (int i = 0; i < wordsList.Count; i++)
                {
                    if (wordsList[i].getCount() > highestCount)
                    {
                        highestCount = wordsList[i].getCount();
                    }
                }

                foreach (WordsFound wordFound in wordsList)
                {
                    if(wordFound.getCount() >= highestCount)
                    {
                        mostUsedWords.Add(wordFound);
                    }
                }

                // Build the returned string
                string returnedString = "";

                if (mostUsedWords.Count == 1)
                {
                    returnedString = $"Le mot qui est le plus répété est \"{mostUsedWords[0].getName()}\" avec {mostUsedWords[0].getCount()} répétitions.";
                }
                else if (mostUsedWords.Count > 1)
                {
                    returnedString = "Les mots les plus répétés sont : \"";
                    foreach (WordsFound wordFound in mostUsedWords)
                    {
                        returnedString += wordFound.getName() + "\", \"";
                    }
                    returnedString = returnedString.Remove(returnedString.Length - 4, 3);
                    returnedString = returnedString + $" avec {highestCount} répétitions.";
                }

                return returnedString;

            }

            Console.WriteLine(mostUsedWord("Ceci est un paragraphe.!?, baby un un un paragraphe destiné baby paragraphe à. baby baby tester quel mot est le plus représenté du paragraphe.", ["est", "le"]));
        }
    }
}
