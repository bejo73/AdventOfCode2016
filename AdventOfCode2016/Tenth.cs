using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Tenth
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\Tenth.txt");

            Dictionary<int, Bot> bots = new Dictionary<int, Bot>();

            Queue<int> q = new Queue<int>();

            Dictionary<int, int> outputs = new Dictionary<int, int>();

            while ((line = file.ReadLine()) != null)
            {
                Match m = Regex.Match(line, "value ([0-9]*) goes to bot ([0-9]*)");
                if (m.Success)
                {
                    int v = Int32.Parse(m.Groups[1].Value);
                    int b = Int32.Parse(m.Groups[2].Value);

                    if (bots.ContainsKey(b))
                    {
                        Bot bot = bots[b];
                        if (bot.ReceiveMicroship(v))
                        {
                            q.Enqueue(b);
                        }
                    }
                    else
                    {
                        Bot bot = new Bot(b);
                        bot.ReceiveMicroship(v);
                        bots.Add(b, bot);
                    }

                    continue;
                }

                m = Regex.Match(line, "bot ([0-9]*) gives low to (bot|output) ([0-9]*) and high to (bot|output) ([0-9]*)"); // ([bot|output]) ([0-9]*) and high to ([bot|output]) ([0-9]*)");
                if (m.Success)
                {
                    int l = Int32.Parse(m.Groups[1].Value);
                    string s = m.Groups[2].Value;
                    int h = Int32.Parse(m.Groups[3].Value);
                    string s1 = m.Groups[4].Value;
                    int h1 = Int32.Parse(m.Groups[5].Value);

                    if (bots.ContainsKey(l))
                    {
                        Bot bot = bots[l];

                        if (s.Equals("bot"))
                        {
                            bot.BotIdLow = h;
                        }
                        else
                        {
                            bot.OutputLow = h;
                            if (!outputs.ContainsKey(h))
                            {
                                outputs.Add(h, 0);
                            }
                        }
                        if (s1.Equals("bot"))
                        {
                            bot.BotIdHigh = h1;
                        }
                        else
                        {
                            bot.OutputHigh = h1;
                            if (!outputs.ContainsKey(h1))
                            {
                                outputs.Add(h1, 0);
                            }
                        }
                    }
                    else
                    {
                        Bot bot = new Bot(l);

                        if (s.Equals("bot"))
                        {
                            bot.BotIdLow = h;
                        }
                        else
                        {
                            bot.OutputLow = h;
                            if (!outputs.ContainsKey(h))
                            {
                                outputs.Add(h, 0);
                            }
                        }

                        if (s1.Equals("bot"))
                        {
                            bot.BotIdHigh = h1;
                        }
                        else
                        {
                            bot.OutputHigh = h1;
                            if (!outputs.ContainsKey(h1))
                            {
                                outputs.Add(h1, 0);
                            }
                        }

                        bots.Add(l, bot);
                    }

                    continue;
                }

                Console.WriteLine("No match");
            }

            while (q.Count > 0)
            {
                int b = q.Dequeue();
                Bot bot = bots[b];
                if (bot.BotIdHigh > int.MinValue)
                {
                    Bot highBot = bots[bot.BotIdHigh];
                    if (highBot.ReceiveMicroship(bot.MicroshipHigh))
                    {
                        q.Enqueue(bot.BotIdHigh);
                    }
                }
                else
                {
                    outputs[bot.OutputHigh] = bot.MicroshipHigh;
                }

                if (bot.BotIdLow > int.MinValue)
                {
                    Bot lowBot = bots[bot.BotIdLow];
                    if (lowBot.ReceiveMicroship(bot.MicroshipLow))
                    {
                        q.Enqueue(bot.BotIdLow);
                    }
                }
                else
                {
                    outputs[bot.OutputLow] = bot.MicroshipLow;
                }

                bot.ResetMicroships();
            }

            Console.WriteLine(outputs[0] * outputs[1] * outputs[2]);
        }
    }
}