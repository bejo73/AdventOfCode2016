using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2017
{
    class Day23
    {
        public static void Run()
        {
            Dictionary<string, long> registers = new Dictionary<string, long>();

            string line;
            StreamReader file = new StreamReader(@".\Data\23.txt");
            List<string> intstructions = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                intstructions.Add(line);
            }

            long multiplyCounter = 0;

            // Part II - Optimize
            //registers["a"] = 1;

            for (int i = 0; i < intstructions.Count; i++)
            {
                string str = intstructions[i];
                Match m = Regex.Match(intstructions[i], "(set|sub|mul|jnz) ([-0-9a-z]{1,1})[ ]{0,1}([-0-9a-z]*)");
                if (m.Success)
                {
                    string instruction = m.Groups[1].Value;
                    string argument1 = m.Groups[2].Value;
                    string argument2 = m.Groups[3].Value;

                    if (Regex.Matches(argument1, @"[a-zA-Z]").Count == 1)
                    {

                        if (!registers.Any(r => r.Key == argument1))
                        {
                            registers[argument1] = 0;
                        }
                    }

                    switch (instruction)
                    {
                        case "set":
                            long second = 0;
                            if (!Int64.TryParse(argument2, out second))
                            {
                                second = registers[argument2];
                            }
                            registers[argument1] = second;
                            break;

                        case "sub":
                            second = 0;
                            if (!Int64.TryParse(argument2, out second))
                            {
                                second = registers[argument2];
                            }
                            registers[argument1] = registers[argument1] - second;
                            break;

                        case "mul":
                            multiplyCounter++;
                            second = 0;
                            if (!Int64.TryParse(argument2, out second))
                            {
                                second = registers[argument2];
                            }
                            registers[argument1] = registers[argument1] * second;
                            break;

                        case "jnz":
                            bool jump = false;

                            long first = 0;

                            if (!Int64.TryParse(argument1, out first))
                            {
                                first = registers[argument1];
                            }

                            if (first != 0)
                            {
                                jump = true;
                            }

                            if (jump)
                            {
                                int steps = 0;
                                if (Int32.TryParse(argument2, out steps))
                                {
                                    if (steps > 0)
                                    {
                                        i = i + (steps - 1);
                                    }
                                    else if (steps < 0)
                                    {
                                        i = i + (steps - 1);
                                    }
                                }
                            }
                            break;
                    }
                }
            }

            Console.WriteLine("Day23 (1): " + multiplyCounter);

            // Part II
            int h = 0;
            int b = 108100;
            int c = b + 17000;

            for (; b <= c; b += 17)
            {
                for (int d = 2; d <= b / 2; d++)
                {
                    if (b % d == 0)
                    {
                        h++;
                        break;
                    }
                }
            }

            Console.WriteLine("      (2): " + h);
        }
    }
}