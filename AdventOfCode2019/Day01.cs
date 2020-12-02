using System;
using System.IO;

namespace AdventOfCode2019
{
    public class Day01
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\1.txt");

            int c1 = 0;
            int c2 = 0; 

            while ((line = file.ReadLine()) != null)
            {
                decimal d = Decimal.Parse(line);
                int fuel = (Convert.ToInt32(Math.Floor(d / 3)) - 2);
                c1 += fuel;

                while (fuel > 0)
                {
                    c2 += fuel;
                    fuel = (Convert.ToInt32(Math.Floor(Decimal.Parse("" + fuel) / 3)) - 2);
                }
            }
            
            Console.WriteLine("Day1 (1): " + c1);
            Console.WriteLine("     (2): " + c2);
        }
    }
}