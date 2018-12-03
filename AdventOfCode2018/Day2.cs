using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018
{
    class Day2
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\2.txt");

            int twos = 0;
            int threes = 0;

            string part2 = "";

            List<string> lines = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                Dictionary<char, int> list = new Dictionary<char, int>();

                foreach (char c in line)
                {
                    if (list.ContainsKey(c))
                    {
                        list[c] = list[c] + 1;
                    }
                    else
                    {
                        list.Add(c, 1);
                    }
                }

                if (list.ContainsValue(2))
                    twos++;

                if (list.ContainsValue(3))
                    threes++;

                lines.Add(line);
            }

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    int counter = 0;

                    for (int k = 0; k < lines[i].Length; k++)
                    {
                        if (lines[i][k] != lines[j][k])
                        {
                            counter++;
                        }
                    }

                    if (counter == 1)
                    {
                        part2 = lines[i] + ", " + lines[j];
                    }

                }
            }

            Console.WriteLine("Day2 (1): " + twos * threes);
            Console.WriteLine("     (2): " + part2);
        }
    }
}