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

            int TURNS_PART_1 = 2020;
            int TURNS_PART_2 = 30000000;

            int[] spokenNumbers = new int[TURNS_PART_2];

            int index = 0;
            int lastNumberSpoken = 0;
            while ((line = file.ReadLine()) != null)
            {
                string[] strArray = line.Split(',');

                for (int a = 0; a < strArray.Length; a++)
                {
                    numbers.Add(new Number() { value = int.Parse(strArray[a]), turn = a + 1 });
                    
                    index++;
                    lastNumberSpoken = int.Parse(strArray[a]);
                    spokenNumbers[lastNumberSpoken] = index;
                }

                // reset 
                spokenNumbers[lastNumberSpoken] = 0;
            }

            // Save to part 2
            int lastNumberSpokenStart = lastNumberSpoken;
            int turnsStart = numbers.Count; ;

            // Part 1 
            int turn = numbers.Count;
            int previousTurn = turn - 1;

            while (turn <= TURNS_PART_1)
            {
                turn++;

                lastNumberSpoken = numbers.Last().value;
                previousTurn = turn - 1;

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

            // Part 2 - Optimized
            turn = turnsStart;
            lastNumberSpoken = lastNumberSpokenStart;

            int spoken = 0;

            while (turn <= TURNS_PART_2)
            {
                previousTurn = turn;
                turn++;
                
                if (spokenNumbers[lastNumberSpoken] == 0)
                {
                    spoken = 0;             
                }
                else
                {
                    spoken = previousTurn - spokenNumbers[lastNumberSpoken];
                }

                spokenNumbers[lastNumberSpoken] = previousTurn;
                lastNumberSpoken = spoken;
            }

            int maxValue = spokenNumbers.Max();
            int maxIndex = spokenNumbers.ToList().IndexOf(maxValue);

            Console.WriteLine("      (2): " + maxIndex);
        }
    }

    class Number
    {
        public int value;
        public int turn;
    }
}