using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace wordCount
{
    class Program
    {
        static Dictionary<string, int> parseText(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    // Read the entire file
                    String line = sr.ReadToEnd();

                    // Split the file into words
                    String[] words = line.Split(null);

                    // Initialize the key value pair of wordCounts
                    Dictionary<string, int> wordCounts = new Dictionary<string, int>();

                    // Loop through each word in the text
                    for (int i = 0; i < words.Length; i++)
                    {
                        // strip punctuation from the string
                        Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                        words[i] = rgx.Replace(words[i], "");

                        // make case irrelavent
                        words[i] = words[i].ToLower();

                        // if the word already exists in the dictionary,
                        // increase the count by 1
                        if (wordCounts.ContainsKey(words[i]))
                        {
                            wordCounts[words[i]]++;
                        }

                        // if the word does not already exist in the dictionary
                        // add it to the dictionary and set the count to 1                             
                        else
                        {
                            wordCounts.Add(words[i], 1);
                        }
                    }
                    return wordCounts;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file couldnot be read: ");
                Console.WriteLine(e.Message);

                Dictionary<string, int> blank = new Dictionary<string, int>();
                return blank;
            }
        }
        static void Main(string[] args)
        {
            Dictionary<string, int> unsorted = parseText("../../example.txt");

            // Sort the dictionary for readability
            var sortedDict = from entry in unsorted orderby entry.Value ascending select entry;

            foreach (KeyValuePair<string, int> kvp in sortedDict)
            {
                Console.WriteLine(kvp.Key + " " + kvp.Value);
            }
            Console.ReadKey();
        }
    }
}
