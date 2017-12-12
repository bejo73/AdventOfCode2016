using AdventOfCode2017.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2017
{
    class Day7
    {
        static List<Program> programs;

        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\7.txt");

            programs = new List<Program>();

            while ((line = file.ReadLine()) != null)
            {
                string pattern = @"([a-z]*) \(([0-9]*)\)[ ->]*([ ,a-z]*)";

                MatchCollection matches = Regex.Matches(line, pattern);

                foreach (Match match in matches)
                {
                    string name = match.Groups[1].Value.Trim();
                    int weight = Int32.Parse(match.Groups[2].Value);

                    Program program = programs.FirstOrDefault(prp => prp.Name == name);

                    // Update existing 
                    if (program != null)
                    {
                        program.Weight = weight;
                    }
                    // Create new
                    else
                    {
                        program = new Program()
                        {
                            Name = name,
                            Weight = weight
                        };

                        programs.Add(program);
                    }

                    string above = match.Groups[3].Value;

                    if (!string.IsNullOrEmpty(above))
                    {
                        string[] prgs = above.Split(',');
                        program.Above = new List<string>();

                        for (int i = 0; i < prgs.Length; i++)
                        {
                            program.Above.Add(prgs[i].Trim());
                        }

                        foreach (string pr in program.Above)
                        {
                            Program aboveProgram = programs.FirstOrDefault(apr => apr.Name == pr);

                            // Update existing
                            if (aboveProgram != null)
                            {
                                aboveProgram.Under = program.Name;
                            }
                            // Create new
                            else
                            {
                                // Create new
                                aboveProgram = new Program()
                                {
                                    Name = pr,
                                    Under = program.Name
                                };
                                programs.Add(aboveProgram);
                            }
                        }
                    }

                    // Console.Write("{0} ", match.Groups[1].Value);
                    // Console.Write("{0} ", match.Groups[2].Value);
                    // Console.Write("{0} ", match.Groups[3].Value);
                    // Console.WriteLine();
                }

            }

            Program bottom = programs.Find(p => p.Under == null);
            Console.WriteLine("Day7 (1): " + bottom.Name);


            foreach (var o in bottom.Above)
            {
                Program a = programs.Find(pr => pr.Name == o.Trim());

                Console.WriteLine("START: " + a.Name);

                if (a != null)
                {
                    int w = getWeight(a);

                    Console.WriteLine("END: " + a.Name + ", a.weight: " + a.Weight + ", getWeight(a): " + w);
                }

            }

            Console.WriteLine("     (2): ");
        }

        private static int getWeight(Program p)
        {
            int weight = p.Weight;

            //Console.Write(" start ("+p.Name+"): " + weight);

            int aboveWeight = 0;

            if (p.Above != null)
            {
                foreach (string item in p.Above)
                {
                    Program a = programs.Find(pr => pr.Name == item);
                    aboveWeight = aboveWeight + getWeight(a);  
                }

                //                Console.Write(" above (" + p.Name + "): " + aboveWeight);

                if ((aboveWeight % p.Above.Count) != 0)
                {
                    Console.WriteLine("" + aboveWeight % p.Above.Count);
                    Console.WriteLine("p.Weight: " + p.Weight + ", aboveWeight: " + aboveWeight + ", name: "+ p.Name);

                    foreach(string item in p.Above)
                    {
                        Program a = programs.Find(pr => pr.Name == item);
                        Console.WriteLine("" + a.Weight + ", " + getWeight(a));
                    }
                }
                else
                {
                    //Console.WriteLine("p.Weight: " + p.Weight);
                }
                
                weight = weight + aboveWeight;
            }
            else
            {

            }
            //Console.WriteLine(" ******************************* name: " + p.Name + "Weight:" + weight);


            //if (weight p.Weight + aboveWeight))
            //{
               // Console.WriteLine("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH " + p.Weight + ", " + aboveWeight + ", " + weight);
            //}

  //          Console.WriteLine(" end (" + p.Name + "): " + weight);
            return weight;
        }

    }
}