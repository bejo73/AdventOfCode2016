using AdventOfCode2017.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using System.Linq;

namespace AdventOfCode2017
{
    class Day8
    {
        static List<Program> programs;

        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\8.txt");

            programs = new List<Program>();



            Dictionary<string, int> register = new Dictionary<string, int>();
            int max = 0;

            while ((line = file.ReadLine()) != null)
            {
                //b inc 5 if a > 1
                //reg1 instruction value1 condition reg2 operator value2

                string pattern = @"([a-z]*) ([a-z]*) ([-0-9]*) ([a-z]*) ([a-z]*) ([<>=!]*) ([-0-9]*)";

                MatchCollection matches = Regex.Matches(line, pattern);

                foreach (Match match in matches)
                {
                    string register1 = match.Groups[1].Value;
                    string instruction = match.Groups[2].Value;
                    int value1 = Int32.Parse(match.Groups[3].Value);
                    string condition = match.Groups[4].Value;
                    string register2 = match.Groups[5].Value;
                    string operation = match.Groups[6].Value;
                    int value2 = Int32.Parse(match.Groups[7].Value);

                    // Add register
                    if (!register.Keys.Any(key => key.Equals(register1)))
                    {
                        register.Add(register1, 0);
                    }

                    if (!register.Keys.Any(key => key.Equals(register2)))
                    {
                        register.Add(register2, 0);
                    }

                    int r2 = register[register2];

                    int r1 = register[register1];

                    bool execute = false;

                    switch (operation)
                    {
                        case ">":
                            if (r2 > value2)
                            {
                                execute = true;
                            }
                            break;
                        case ">=":
                            if (r2 >= value2)
                            {
                                execute = true;
                            }
                            break;
                        case "<":
                            if (r2 < value2)
                            {
                                execute = true;
                            }
                            break;
                        case "<=":
                            if (r2 <= value2)
                            {
                                execute = true;
                            }
                            break;
                        case "==":
                            if (r2 == value2)
                            {
                                execute = true;
                            }
                            break;
                        case "!=":
                            if (r2 != value2)
                            {
                                execute = true;
                            }
                            break;
                    }

                    if (execute)
                    {
                        switch (instruction)
                        {
                            case "inc":
                                r1 = r1 + value1;
                                break;
                            case "dec":
                                r1 = r1 - value1;
                                break;
                        }


                        if (r1 > max)
                        {
                            max = r1;
                        }
                       // Console.WriteLine("r1: " + r1);
                        register[register1] = r1;
                    }

                    


                    //Console.WriteLine("register1: " + register1);
                    //Console.WriteLine("r2: " + r2);
 
                }

            }
            // d.FirstOrDefault(x => x.Value == d.Values.Max()).Key;
            int largest = register.FirstOrDefault(x => x.Value == register.Values.Max()).Value;

            Console.WriteLine("Day8 (1): " + largest);
            Console.WriteLine("     (2): " + max);
        }

        private static int getWeight(Program p)
        {
            int weight = p.Weight;

            Console.Write(" start ("+p.Name+"): " + weight);
            int aboveWeight = 0;
            if (p.Above != null)
            {
                HashSet<int> hi = new HashSet<int>();

                
                foreach (string item in p.Above)
                {
                    Program a = programs.Find(pr => pr.Name == item);
                    aboveWeight = aboveWeight + getWeight(a);

                    hi.Add(weight);
                }

                Console.Write(" above (" + p.Name + "): " + aboveWeight);



                weight = weight + aboveWeight;

                
            }
            else
            {

            }
            //Console.WriteLine(" ******************************* name: " + p.Name + "Weight:" + weight);


            //if (weight p.Weight + aboveWeight))
            //{
                Console.WriteLine("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH " + p.Weight + ", " + aboveWeight + ", " + weight);
            //}

            Console.WriteLine(" end (" + p.Name + "): " + weight);
            return weight;
        }

    }
}