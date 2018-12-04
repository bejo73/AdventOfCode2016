using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018
{
    class Day05
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\5.txt");

            List<int> maze = new List<int>();
            List<int> maze2 = new List<int>();

            while ((line = file.ReadLine()) != null)
            {
                maze.Add(Int32.Parse(line));
                maze2.Add(Int32.Parse(line));
            }

            // Part 1
            int position = 0;
            int steps = 0;

            while (0 <= position && position < maze.Count)
            {
                int offset = maze[position];

                maze[position]++;

                position = position + offset;
                steps++;
            }

            Console.WriteLine("Day5 (1): " + steps);

            // Part 2
            position = 0;
            steps = 0;

            while (0 <= position && position < maze2.Count)
            {
                int offset = maze2[position];

                if (offset >= 3)
                {
                    maze2[position]--;
                }
                else
                {
                    maze2[position]++;
                }

                position = position + offset;
                steps++;
            }

            Console.WriteLine("     (2): " + steps);
        }
    }
}