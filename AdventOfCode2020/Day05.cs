using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    public class Day05
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day05.txt");

            int high = 0;
            int seat = 0;

            List<int> ids = new List<int>();

            while ((line = file.ReadLine()) != null)
            {
                string strBin = line.Substring(0, 7).Replace('F', '0').Replace('B', '1');
                int row = Convert.ToInt32(strBin, 2);

                strBin = line.Substring(7, 3).Replace('L', '0').Replace('R', '1');
                int col = Convert.ToInt32(strBin, 2);

                int id = row * 8 + col;
                if (id > high)
                    high = id;

                ids.Add(id);

            }

            ids.Sort();

            for (int i = 1; i < ids.Count-1; i++)
            {
                if (ids[i - 1] + 2 != ids[i + 1])
                {
                    seat = ids[i] + 1;
                    break;

                    //Console.WriteLine(ids[i]);
                }
            }

            Console.WriteLine("Day2 (1): " + high);
            Console.WriteLine("     (2): " + seat);
        }
    }
}