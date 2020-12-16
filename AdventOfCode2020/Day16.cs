using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day16
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day16.txt");

            HashSet<int> validNumbers = new HashSet<int>();

            Regex r = new Regex("([0-9]*)-([0-9]*)");
            while ((line = file.ReadLine()).Trim().Length > 0)
            {
                var matches = r.Matches(line);

                foreach (Match m in matches)
                {
                    int start = int.Parse(m.Groups[1].Value);
                    int end = int.Parse(m.Groups[2].Value);
                 
                    for (int i = start; i <= end; i++)
                        validNumbers.Add(i);
                }
            }

            while ((line = file.ReadLine()).Trim().Length > 0)
            {
                //Console.WriteLine(line);
            }

            List<int> allInvalidValues = new List<int>();
            line = file.ReadLine();
            while ((line = file.ReadLine()) != null)
            {
                string[] strArr = line.Split(',');

                for (int a = 0; a < strArr.Length; a++)
                {
                    int n = int.Parse(strArr[a]);
                    if (!validNumbers.Contains(n))
                        allInvalidValues.Add(n);
                }
            }

            Console.WriteLine("Day10 (1): " + allInvalidValues.Sum());
            Console.WriteLine("      (2): " + "TBD");
        }
    }
}