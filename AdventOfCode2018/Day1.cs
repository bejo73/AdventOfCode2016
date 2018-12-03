using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018
{
    public class Day1
    {
        public static void Run()
        {
            string change;
            HashSet<int> uniqueFrequencies = new HashSet<int>();

            int frequency = 0;
            int frequency1 = 0;

            bool notFound = true;

            bool firstRound = true;
            using (StreamReader frequencyChangesReader = new StreamReader(@".\Data\1.txt"))
            {
                while (notFound)
                {
                    while ((change = frequencyChangesReader.ReadLine()) != null)
                    {
                        int f = Int32.Parse(change.Trim());
                        
                        if (!uniqueFrequencies.Add(frequency))
                        {
                            Console.WriteLine(frequency);
                            notFound = false;
                            break;
                        }

                        frequency = frequency + f;    
                    }

                    if (firstRound)
                    {
                        frequency1 = frequency;
                        Console.WriteLine(frequency);
                        firstRound = false;
                    }

                    frequencyChangesReader.DiscardBufferedData();
                    frequencyChangesReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    frequencyChangesReader.Read();
                }             
            }

            Console.WriteLine("Day1 (1): " + frequency1);
            Console.WriteLine("     (2): " + frequency);
        }
    }
}