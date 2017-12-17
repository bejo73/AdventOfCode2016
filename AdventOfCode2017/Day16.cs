using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    class Day16
    {
        public static void Run()
        {
            char[] programs = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p' };
            //char[] programs = { 'a', 'b', 'c', 'd', 'e' };
            char[] tmp = new char[programs.Length];
            char[] org = new char[programs.Length];
            Array.Copy(programs, org, programs.Length);
            
            StreamReader file = new StreamReader(@".\Data\16.txt");
            string line = file.ReadLine();
            string[] split = line.Split(',');

            string firstCombination = "";
            HashSet<string> combinations = new HashSet<string>();
            long index = 0;

            for (long i = 0; i < 1000000000; i++)
            {
                foreach (string move in split)
                {
                    string pattern = @"([sxp]{1,1})([/0-9a-z]*)";

                    MatchCollection matches = Regex.Matches(move, pattern);

                    foreach (Match match in matches)
                    {
                        string type = match.Groups[1].Value;
                        string info = match.Groups[2].Value;

                        switch (type)
                        {
                            case "s":
                                int spin = Int32.Parse(info);
                                Array.Copy(programs, tmp, programs.Length);
                                Array.Copy(tmp, 0, programs, spin, programs.Length - spin);
                                Array.Copy(tmp, programs.Length - spin, programs, 0, spin);
                                break;
                            case "x":
                                string[] x = info.Split('/');
                                int a = Int32.Parse(x[0]);
                                int b = Int32.Parse(x[1]);
                                char tmpChar = programs[a];
                                programs[a] = programs[b];
                                programs[b] = tmpChar;
                                break;
                            case "p":
                                string[] p = info.Split('/');
                                a = Array.IndexOf(programs, Char.Parse(p[0]));
                                b = Array.IndexOf(programs, Char.Parse(p[1]));
                                tmpChar = programs[a];
                                programs[a] = programs[b];
                                programs[b] = tmpChar;
                                break;
                        }
                    }
                }

                StringBuilder s = new StringBuilder();
                foreach (char j in programs)
                {
                    s.Append(j);
                }

                // Pattern repeats
                if (!combinations.Add(s.ToString()))
                {
                    // Pattern repeats, % gives us the remainder up to a billion
                    // Decrease with one, since list starts with zero
                    index = (1000000000 % i) - 1;
                    break;
                }

                if (i == 0)
                {
                    firstCombination = s.ToString();
                }
            }

            List<string> list = combinations.ToList();
            Console.WriteLine(list[(int)index]);

            Console.WriteLine("Day16 (1): " + firstCombination);
            Console.WriteLine("      (2): " + list[(int)index]);
        }
    }
}