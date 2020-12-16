using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day14
    {

        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day14.txt");

            Dictionary<int, long> memory = new Dictionary<int, long>();
            string currentBitmask = null;
            Regex r = new Regex("mem.([0-9]*). = ([0-9]*)");
            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("mask"))
                {
                    currentBitmask = line.Substring(7);
                }
                else
                {
                    Match m = r.Match(line);
                    int mem = int.Parse(m.Groups[1].Value);
                    int val = int.Parse(m.Groups[2].Value);

                    string binVal = Convert.ToString(val, 2).PadLeft(36, '0');
                    StringBuilder result = new StringBuilder(binVal);
                    for (int i = 0; i < 36; i++)
                    {
                        if (currentBitmask[i] == '0')
                        {
                            result[i] = '0';
                        }
                        if (currentBitmask[i] == '1')
                        {
                            result[i] = '1';
                        }   
                    }


                    if (memory.ContainsKey(mem))
                    {
                        memory.Remove(mem);
                    }
                   
                    memory.Add(mem, Convert.ToInt64(result.ToString(), 2));
                    
                }
            }

            var sum = memory.Sum(i => i.Value);
           

            Console.WriteLine("Day10 (1): " + sum);
            Console.WriteLine("      (2): " );
        }

    }
}