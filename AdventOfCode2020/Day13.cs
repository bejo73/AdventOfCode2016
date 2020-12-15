using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    class Day13
    {

        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day13.txt");

            List<string> lines = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }

            List<int> busList = new List<int>();
            string[] strList = lines[1].Split(',');

            foreach (var c in strList)
            {
                if (c != "x")
                    busList.Add(int.Parse(c));
                else
                    busList.Add(-1);                
            }

            long timestamp = 0;

            // Least Common Multiple
            long lcm = busList[0];

            for (int i = 1; i < busList.Count; i++)
            {
                if (busList[i] == -1)
                    continue;

                while ((timestamp + i) % busList[i] != 0)
                {
                    timestamp += lcm;
                }

                lcm *= busList[i];
            }

            Console.WriteLine("Day10 (1): " + "Made on calculator");
            Console.WriteLine("      (2): " + timestamp);
        }

    }
}