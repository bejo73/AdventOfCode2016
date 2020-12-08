using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    class Day07
    {

        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day07_Test.txt");
            
            int part1 = 0;
            int part2 = 0;

            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("Day7 (1): " + part1);
            Console.WriteLine("     (2): " + part2);
        }
    }
}