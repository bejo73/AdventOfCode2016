using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    class Day09
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day09.txt");

            long part1 = 0;
            long part2 = 0;

            List<long> numbers = new List<long>();

            while ((line = file.ReadLine()) != null)
            {
                numbers.Add(long.Parse(line));
            }

            int preamble = 25;
            long prevProd = 0;

            bool found = false;
            long num = 0;
            int faultyPositionInList = 0;

            // Part 1
            for (int i = preamble; i < numbers.Count; i++)
            {
                for (int j = i - preamble; j < i; j++)
                {
                    for (int k = j + 1; k < i; k++)
                    {
                        if (numbers[j] + numbers[k] == numbers[i])
                        {
                            if (prevProd != numbers[j] * numbers[k])
                            {
                                found = true;
                                prevProd = numbers[j] * numbers[k];
                            }
                        }
                        else
                        {
                            num = numbers[i];
                        }

                        if (found) break;
                    }

                    if (found) break;
                }

                if (!found)
                {
                    part1 = num;
                    faultyPositionInList = i;
                    break;
                }

                found = false;
            }

            // Part 2
            for (int g = 0; g < faultyPositionInList; g++)
            {
                int y = g + 1;
                long sum = numbers[g];
                while (true)
                {
                    sum += numbers[y];
                    if (sum > num)
                    {
                        break;
                    }
                    else if (sum == num)
                    {
                        List<long> faultyRange = new List<long>();
                        faultyRange = numbers.GetRange(g, y - g);

                        long max = faultyRange.Max();
                        long min = faultyRange.Min();

                        part2 = max + min;
                    }
                    else
                    {
                        y++;
                    }
                }
            }

            Console.WriteLine("Day9 (1): " + part1);
            Console.WriteLine("     (2): " + part2);
        }
    }
}