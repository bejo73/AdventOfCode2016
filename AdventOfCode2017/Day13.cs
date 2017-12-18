using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2017
{
    class Day13
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\13_Test_1.txt");

            List<Layer> layers = new List<Layer>();

            int depthCheck = 0;

            while ((line = file.ReadLine()) != null)
            {
                string pattern = @"([0-9]*): ([0-9]*)";

                MatchCollection matches = Regex.Matches(line, pattern);

                foreach (Match match in matches)
                {
                    int depth = Int32.Parse(match.Groups[1].Value);
                    int range = Int32.Parse(match.Groups[2].Value);
                    //         Console.WriteLine("depth: " + depth + ", range: " + range + ", dc" + depthCheck);

                    if (depthCheck != depth)
                    {
                        for (int i = depthCheck; i < depth; i++)
                        {
                            layers.Add(new Layer(0));
                            depthCheck++;
                        }
                    }

                    layers.Add(new Layer(range));

                    depthCheck++;
                }
            }

            Console.WriteLine();


            int c = 0;
            foreach (Layer l in layers)
            {
                Console.WriteLine("depth: " + c + ", range: " + l.range + ", current: " + l.current + ", next: " + l.next);
                c++;
            }

            int answer = 0;

            List<Layer> org = layers;
            int wait = 0;
            int wwait = 0;
            bool ready = false; 
            while (!ready)
            {


                for (int step = 0; step < layers.Count; step++)
                {
                    if ((layers[step].current == 0) && layers[step].range > 0)
                    {
                        answer = answer + layers[step].range * step;
                        Console.WriteLine("Crash!!! " + step);


                        wwait++;
                        break;
                        //wait = wwait;
                        //step = 0;
                        //layers = org;
                    }

                    foreach (Layer l in layers)
                    {

                        if (l.range > 0)
                        {
                            if (l.next > l.current)
                            {
                                l.current = l.next;
                                if (l.next == (l.range - 1))
                                {
                                    l.next--;
                                }
                                else
                                {
                                    l.next++;
                                }
                            }
                            else
                            {
                                l.current = l.next;
                                if (l.next == 0)
                                {
                                    l.next++;
                                }
                                else
                                {
                                    l.next--;
                                }
                            }
                        }
                    }
                   
                }
            }

            Console.WriteLine("Day13 (1): " + answer);
            Console.WriteLine("      (2): " + wwait);
        }
    }

    class Layer
    {
        public int range;
        public int current;
        public int next;

        public Layer(int range)
        {
            this.range = range;
            current = 0;

            if (range > 1)
            {
                next = 1;
            }
            else
            {
                next = 0;
            }
        }
    }
}