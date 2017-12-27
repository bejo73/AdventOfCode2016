using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Threading;

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


            // Part II
           Queue<long> aQueue = new Queue<long>();
           Queue<long> bQueue = new Queue<long>();

            Thread thread1 = new Thread(() => DoWork(lines, aQueue, bQueue, 0, 1000));
            thread1.Start();

            Thread.Sleep(1000);
            Thread thread2 = new Thread(() => DoWork(lines, bQueue, aQueue, 1, 100));

            
            thread2.Start();
        }

        public static void DoWork(List<string> lines, Queue<long> sndQ, Queue<long> rcvQ, int id, int sl)
        {
            Dictionary<string, long> registers = new Dictionary<string, long>();

            registers["p"] = id;
            int sends = 0;
            //long lastPLayed = 0;
            bool firstFound = false;
            for (int i = 0; i < lines.Count; i++)
            {
                string str = lines[i];
                Match m = Regex.Match(lines[i], "(snd|set|add|mul|mod|rcv|jgz) ([a-z0-9]{1,1})[ ]{0,1}([-0-9a-z]*)");
                if (m.Success)
                {
                    string instruction = m.Groups[1].Value;
                    string argument1 = m.Groups[2].Value;
                    string argument2 = m.Groups[3].Value;

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
                                int cc = 0;
                            //while (true)
                            // {
                            if (rcvQ.Count > 0)
                            {
                                lock (rcvQ)
                                {
                                    registers[argument1] = rcvQ.Dequeue();
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("id: " + id + ", sends: " + sends);
                                return;
                                //Thread.Sleep(10);
                            }
                    }
                                

                                //if (cc > 5)
                                //{
                                //    firstFound = true;
                                //     break;
                                //}
                            //}
                                /*if (registers[argument1] != 0)
                            {
                                //Console.WriteLine("Play rcv: " + lastPLayed);
                                //firstFound = true;
                                Console.WriteLine("Rcv ("+id+") Q: " + rcvQ.Dequeue());
                            }*/
                            //Console.WriteLine("Rcv (" + id + ") Q: " + registers[argument1]);
                            break;

                        case "snd":
                            //lastPLayed = registers[argument1];
                            sends++;
                            v = 0;
                            if (!Int64.TryParse(argument1, out v))
                            {
                                lock (sndQ)
                                {

                                    sndQ.Enqueue(registers[argument1]);
                                }
                                //Console.WriteLine("Snd (" + id + ") Q: " + registers[argument1].ToString());
                            }
                            else
                            {
                                lock (sndQ)
                                {

                                    sndQ.Enqueue(v);
                                }
                                //Console.WriteLine("Snd (" + id + ") Q: " +v);
                            }
                            //registers[argument1] = registers[argument1] + v;

                            
                            //sndQ.Enqueue(registers[argument1]);
                            //Thread.Sleep(sl);
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
                        Console.WriteLine("id: " + id + ", sends: "+ sends);
                        break;
                    }
                }
            }

        }

    }

  

}