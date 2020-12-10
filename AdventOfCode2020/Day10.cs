using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    class Day10
    {
        static Dictionary<int, long> cache = new Dictionary<int, long>();

        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day10.txt");

            List<int> adapters = new List<int>();

            while ((line = file.ReadLine()) != null)
            {
                adapters.Add(int.Parse(line));
            }

            adapters.Sort();

            int currentJolt = 0;

            int one = 0;
            int three = 0;

            foreach (var a in adapters)
            {
                int diff = a - currentJolt;

                switch (diff)
                {
                    case 1:
                        one++;
                        break;
                    case 3:
                        three++;
                        break;
                    default:
                        break;
                }

                currentJolt = a;
            }
            three++;

            currentJolt = 0;            
            long distinctWays = Sum(adapters, currentJolt) + 1;

            Console.WriteLine("Day10 (1): " + (one * three));
            Console.WriteLine("      (2): " + distinctWays);
        }

        public static long Sum(List<int> adapters, int currentJolt)
        {
            if (cache.ContainsKey(currentJolt))
            {
                return cache.FirstOrDefault(f => f.Key == currentJolt).Value;
            }

            List<int> nextAdapters = adapters.Where(i => i <= (currentJolt + 3)).ToList();

            long sum = 0;
            if (nextAdapters.Count > 1)
            {
                sum = nextAdapters.Count - 1;
            }

            int index = 0;
            foreach (var t in nextAdapters)
            {
                index++;
                List<int> remainingAdapters = adapters.GetRange(index, adapters.Count - index);
                long tmpSum = Sum(remainingAdapters, t);
                sum += tmpSum;

                if (!cache.ContainsKey(t))
                {
                    cache.Add(t, tmpSum);
                }
            }

            return sum;
        }
    }
}