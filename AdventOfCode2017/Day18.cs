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
            List<string> instructions = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                instructions.Add(line);
            }

            long lastPLayed = 0;
            bool firstFound = false;
            for (int i = 0; i < instructions.Count; i++)
            {
                string str = instructions[i];
                Match m = Regex.Match(instructions[i], "(snd|set|add|mul|mod|rcv|jgz) ([a-z]{1,1})[ ]{0,1}([-0-9a-z]*)");
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
                                    i = i + steps - 1;
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


            Thread thread1 = new Thread(() => DoWork(instructions, aQueue, bQueue, 0));
            thread1.Start();

            Thread thread2 = new Thread(() => DoWork(instructions, bQueue, aQueue, 1));
            thread2.Start();
            
            while (thread1.IsAlive || thread2.IsAlive)
            {
                Console.WriteLine("aQ: " + aQueue.Count + ", bQ: " + bQueue.Count);
            }

            /*Queue<long> q1 = new Queue<long>();
            Queue<long> q2 = new Queue<long>();

            // Part  II Again
            
            AssemblyProgram p1 = new AssemblyProgram("0", q1, q2, instructions);
            AssemblyProgram p2 = new AssemblyProgram("1", q2, q1, instructions);
            
            // create a ThreadStart delegate and pass in the method that will run 
            // (similar to run on Java's Runnable)
            Thread t1 = new Thread(new ThreadStart(p1.Run));
            t1.Start();


            Thread t2 = new Thread(new ThreadStart(p2.Run));
            t2.Start();
            
            Console.WriteLine(q1.Count + ", " + q2.Count);*/
        }

        public static Queue<long> aQueue = new Queue<long>();
        public static Queue<long> bQueue = new Queue<long>();

        public static void DoWork(List<string> lines, Queue<long> sndQ, Queue<long> rcvQ, int id)
        {
            Dictionary<string, long> registers = new Dictionary<string, long>();

            registers["p"] = id;
            int sends = 0;
            int rcvs = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                //Console.WriteLine("id: " + id + ", sndQ: " + sndQ.Count + ", rcvQ: " + rcvQ.Count);

                string str = lines[i];
                Match m = Regex.Match(lines[i], "(snd|set|add|mul|mod|rcv|jgz) ([a-z0-9]{1,1})[ ]{0,1}([-0-9a-z]*)");
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
                            rcvs++;
                            
                            //if (registers[argument1] != 0)
                            //{
                            if (rcvQ.Count > 0)
                            {
                                registers[argument1] = rcvQ.Dequeue();
                                break;
                            }
                            else
                            {
                                
                                if (rcvQ.Count == 0 && sndQ.Count == 0)
                                {
                                    Console.WriteLine("id: " + id + ", sends: " + sends + ", rcvs: " + rcvs);
                                    return;
                                }
                                
                                i--;
                                
                            }
                        //}
                        break;

                        case "snd":

                            v = 0;

                            if (!Int64.TryParse(argument1, out v))
                            {
                                v = registers[argument1];
                            }

                            sndQ.Enqueue(v);
                            
                            sends++;

                            break;

                        case "jgz":
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
                                    i = i + steps - 1;
                                }
                            }

                            break;
                    }
                }
            }

        }

    }

    class AssemblyProgram
    {
        private string id = string.Empty;

        private Queue<long> sendQueue;
        private Queue<long> receiveQueue;

        List<string> instructions;

        Dictionary<string, long> registers = new Dictionary<string, long>();

        public AssemblyProgram(string id, Queue<long> sendQueue, Queue<long> receiveQueue, List<string> instructions)
        {
            this.id = id;
            this.sendQueue = sendQueue;
            this.receiveQueue = receiveQueue;
            this.instructions = instructions;
        }

        public void  Run()
        {
            registers["p"] = Int64.Parse(id);
            int sends = 0;
            int rcvs = 0;

            for (int i = 0; i < instructions.Count; i++)
            {
                string str = instructions[i];
                Match m = Regex.Match(instructions[i], "(snd|set|add|mul|mod|rcv|jgz) ([a-z]{1,1})[ ]{0,1}([-0-9a-z]*)");
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
                            rcvs++;
                            //if (registers[argument1] != 0)
                            //{
                            if (receiveQueue.Count > 0)
                            {
                                registers[argument1] = receiveQueue.Dequeue();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("id: " + id + ", sends: " + sends + ", rcvs: " + rcvs);
                                return;
                            }
                            //break;

                        case "snd":
                            v = 0;

                            if (!Int64.TryParse(argument1, out v))
                            {
                                v = registers[argument1];
                            }

                            sendQueue.Enqueue(v);
                            sends++;
                            break;

                        case "jgz":
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
                                    i = i + steps - 1;
                                }
                            }

                            break;
                    }


                }


            }

        }

    }
}