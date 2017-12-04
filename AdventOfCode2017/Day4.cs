using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    class Day4
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\4.txt");
            char separator = ' ';

            int validPassphrases1 = 0;
            int validPassphrases2 = 0;

            List<string> validPassphrases1List = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                HashSet<string> hash = new HashSet<string>();
                bool validPassphrase = true;
                string[] words = line.Split(separator);

                foreach (string word in words)
                {
                    if (!hash.Add(word))
                    {
                        validPassphrase = false;
                        break;
                    }
                }

                if (validPassphrase)
                {
                    validPassphrases1++;
                    validPassphrases1List.Add(line);
                }
            }

            foreach (string passPhrase in validPassphrases1List)
            {
                string[] words = passPhrase.Split(separator);
                bool validPassphrase = true;

                for (int i = 0; i < words.Length; i++)
                {
                    for (int j = 0; j < words.Length; j++)
                    {
                        if (i != j)
                        {
                            string sorted1 = String.Concat(words[i].OrderBy(c => c));
                            string sorted2 = String.Concat(words[j].OrderBy(c => c));

                            if (sorted1.Equals(sorted2))
                            {
                                validPassphrase = false;
                                break;
                            }
                        }
                    }
                }

                if (validPassphrase)
                {
                    validPassphrases2++;
                }
            }

            Console.WriteLine("Day4 (1): " + validPassphrases1);
            Console.WriteLine("     (2): " + validPassphrases2);
        }
    }
}