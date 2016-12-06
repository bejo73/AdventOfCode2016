using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Sixth
    {
        public static void Run()
        {
            string line;
            //StreamReader file = new StreamReader(@".\Data\Sixth_test.txt");
            StreamReader file = new StreamReader(@".\Data\Sixth.txt");

            List<string> strings = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (strings.Count > i)
                    {
                        strings[i] = strings[i] + line.Substring(i, 1);
                    }
                    else
                    {
                        strings.Add(line.Substring(i, 1));
                    }
                }
            }

            Console.Write("Six A: ");

            foreach (string str in strings)
            {
                Char c = str.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;
                Console.Write(c.ToString());
            }

            Console.WriteLine();
            Console.Write("Six B: ");

            foreach (string str in strings)
            {
                Char c = str.GroupBy(x => x).OrderByDescending(x => x.Count()).Last().Key;
                Console.Write(c.ToString());
            }
        }
    }
}