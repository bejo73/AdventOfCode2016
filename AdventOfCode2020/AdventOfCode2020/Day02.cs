using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day02
    {

        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day02.txt");

            int validPasswords1 = 0;
            int validPasswords2 = 0;

            while ((line = file.ReadLine()) != null)
            {
                string[] list = line.Split(' ');

                string range = list[0];
                string letter = list[1].Substring(0,1);
                string password = list[2];

                string[] limitList = range.Split('-');

                int min = Int32.Parse(limitList[0]);
                int max = Int32.Parse(limitList[1]);

                int count = password.Count(f => f == Char.Parse(letter));

                if (count >= min && count <= max)
                    validPasswords1++;

                if (password.Substring(min - 1, 1).Equals(letter) ^ password.Substring(max - 1, 1).Equals(letter))
                {
                    validPasswords2++;
                }                
            }

            Console.WriteLine("Day2 (1): " + validPasswords1);
            Console.WriteLine("     (2): " + validPasswords2);
        }

    }
}