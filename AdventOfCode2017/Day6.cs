using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2017
{
    class Day6
    {
        public static void Run()
        {
            //int[] bank = { 0, 2, 7, 0 };
            //StringBuilder key = new StringBuilder("0-2-7-0");

            int[] bank = { 4, 10, 4, 1, 8, 4, 9, 14, 5, 1, 14, 15, 0, 15, 3, 5 };
            String key = getId(bank);

            List<string> list = new List<string>();
            list.Add(key);

            HashSet<string> hash = new HashSet<string>();

            int steps = 0;
           
            while (hash.Add(key))
            {    
                int max = bank.Max();
                int index = bank.ToList().IndexOf(max);

                int d = max / bank.Length ;

                // Clear
                bank[index] = 0;

                // No need to add if zero
                if (d > 0)
                {
                    for (int i = 0; i < bank.Length; i++)
                    {
                        bank[i] = bank[i] + d;
                    }
                }

                for (int k = 1; k <= max % bank.Length; k++)
                {
                    if ((index + k) < bank.Length)
                    {
                        bank[index+k] = bank[index+k] + 1;
                    }
                    else
                    {
                        bank[index + k - bank.Length] = bank[index + k - bank.Length] + 1;
                    }
                }

                key = getId(bank);
                list.Add(key.ToString());
                steps++;
            }

            int cycles = list.Count - list.IndexOf(key.ToString()) - 1;

            Console.WriteLine("Day5 (1): " + steps);
            Console.WriteLine("     (2): " + cycles);
        }

        private static string getId(int[] bank)
        {
            StringBuilder id = new StringBuilder();

            for (int j = 0; j < bank.Length; j++)
            {
                id.Append(bank[j]);
                id.Append("-");
            }

            id.Remove(id.Length - 1, 1);

            return id.ToString();
        }

    }
}