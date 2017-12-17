using System;
using System.Collections.Generic;

namespace AdventOfCode2017
{
    class Day17
    {
        public static void Run()
        {
            int input = 324;
 
            // Part I
            List<int> buffer = new List<int>();
            buffer.Add(0);  
            int current = 0;

            for (int i = 1; i <= 2017; i++)
            {
                int steps = input;
                if (buffer.Count <= input)
                {
                    steps = input % i;
                }

                current = current + steps;

                if (current >= buffer.Count)
                {
                    current = current - buffer.Count;
                }

                if (current < buffer.Count)
                {
                    buffer.Insert(current + 1, i);
                    current++;
                }
                else if (current == buffer.Count)
                {
                    buffer.Add(i);
                    current++;
                }
                else
                {
                    buffer.Insert(current + 1 - buffer.Count, i);
                    current++;
                }
            }

            Console.WriteLine("Day17 (1): " + buffer[buffer.IndexOf(2017) + 1]);

            // Part II
            buffer = new List<int>();
            buffer.Add(0);
            buffer.Add(0);
            current = 0;

            for (int i = 1; i <= 50000000; i++)
            {
                int steps = input;
                if (i <= input)
                {
                    steps = input % i;
                }

                current = current + steps;

                if (current >= i)
                {
                    current = current - i;
                }

                if (current == 0)
                {
                    buffer[1] = i;
                }

                current++;
            }

            Console.WriteLine("      (2): " + buffer[1]);
        }
    }
}