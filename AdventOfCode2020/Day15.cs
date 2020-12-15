using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    class Day15
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day15.txt");

            List<Number> numbers = new List<Number>();

            while ((line = file.ReadLine()) != null)
            {
                string[] strArray = line.Split(',');

                for (int a = 0; a < strArray.Length; a++)
                {
                    numbers.Add(new Number() { value = int.Parse(strArray[a]), turn = a + 1 });
                }
            }

            int turn = numbers.Count;
            int lastNumberSpoken = 0;

            while (turn < 2021)
            {
                turn++;

                lastNumberSpoken = numbers.Last().value;
                int previousTurn = turn - 1;

                if (numbers.Count(n => n.value == lastNumberSpoken) > 1)
                {
                    int recentlySpoken = numbers.Last(n => n.value == lastNumberSpoken && n.turn < previousTurn).turn;
                    numbers.Add(new Number() { value = previousTurn - recentlySpoken, turn = turn });
                }
                else
                {
                    numbers.Add(new Number() { value = 0, turn = turn });
                }
            }

            Console.WriteLine("Day10 (1): " + lastNumberSpoken);
            Console.WriteLine("      (2): " + "TBD");
        }
    }

    class Number
    {
        public int value;
        public int turn;
    }

}