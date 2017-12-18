using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2017
{
    class Day18
    {
        public static void Run()
        {
            Dictionary<string, long> registers = new Dictionary<string, long>();

            string line;
            StreamReader file = new StreamReader(@".\Data\18.txt");
            List<string> lines = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }
            long lastPLayed = 0;
            bool firstFound = false;
            for (int i = 0; i < lines.Count; i++)
            {
                string str = lines[i];
                Match m = Regex.Match(lines[i], "(snd|set|add|mul|mod|rcv|jgz) ([a-z]{1,1})[ ]{0,1}([-0-9a-z]*)");
                if (m.Success)
                {
                    string instruction = m.Groups[1].Value;
                    string argument1   = m.Groups[2].Value;
                    string argument2   = m.Groups[3].Value;

                    if (!registers.Any(r => r.Key == argument1))
                    {
                        registers[argument1] = 0;
                    }

                    switch (instruction)
                    {
                        case "set":
                            long v = 0;
                            if (!Int64.TryParse(argument2, out v))
                            {
                                v = registers[argument2];
                            }
                            registers[argument1] = v;
                            break;

                        case "add":
                            v = 0;
                            if (!Int64.TryParse(argument2, out v))
                            {
                                v = registers[argument2];
                            }
                            registers[argument1] = registers[argument1] + v;
                            break;

                        case "mul":
                            v = 0;
                            if (!Int64.TryParse(argument2, out v))
                            {
                                v = registers[argument2];
                            }
                            registers[argument1] = registers[argument1] * v;
                            break;

                        case "mod":
                            v = 0;
                            if (!Int64.TryParse(argument2, out v))
                            {
                                v = registers[argument2];
                            }
                            registers[argument1] = registers[argument1] % v;
                            break;
                        case "rcv":
                            if (registers[argument1] != 0)
                            {
                                Console.WriteLine("Play rcv: " + lastPLayed);
                                firstFound = true;
                            }
                            break;

                        case "snd":
                            lastPLayed = registers[argument1];
                            break;

                        case "jgz":
                            bool jump = false;

                            if (registers[argument1] > 0)
                            {
                                jump = true;
                                v = 0;
                                if (!Int64.TryParse(argument2, out v))
                                {
                                    v = registers[argument2];
                                }
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

                    if (firstFound)
                    {
                        break;
                    }
                }
            }
        }
    }
}