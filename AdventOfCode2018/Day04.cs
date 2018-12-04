using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2018
{
    class Day04
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\4.txt");

            List<string> list = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                list.Add(line);
            }

            list = list.OrderBy(q => q).ToList();

            List<Guard> guards = new List<Guard>();
            Guard currentGuard = null;
            
            int currentFallAsleepMinute = 0;
            int currentWakeUpMinute = 0;

            foreach (string item in list)
            {
                //[1518-11-01 00:00] Guard #10 begins shift
                string pattern = @"\[([-0-9]*) ([:0-9]*)\] Guard #([0-9]*) begins shift";

                //[1518-11-01 00:05] falls asleep
                string fallAsleepPattern = @"\[([-0-9]*) ([0-9]*):([0-9]*)\] falls asleep";

                //[1518-11-01 00:25] wakes up
                string wakeUpPattern = @"\[([-0-9]*) ([0-9]*):([0-9]*)\] wakes up";

                if (Regex.IsMatch(item, pattern))
                {
                    MatchCollection matches = Regex.Matches(item, pattern);
                    foreach (Match match in matches)
                    {
                        int id = Int32.Parse(match.Groups[3].Value);

                        if (guards.Any(g => g.Id == id))
                        {
                            currentGuard = guards.Find(g => g.Id == id);
                            currentGuard.shifts.Add(new char[60]);
                        }
                        else
                        {
                            char[] c = new char[60];
                            Guard g = new Guard
                            {
                                Id = id,
                                shifts = new List<char[]>()
                            };
                            g.shifts.Add(new char[60]);
                            guards.Add(g);
                            currentGuard = g;
                        }
                        
                        for (int i = 0; i < 60; i++)
                        {
                            currentGuard.shifts.Last()[i] = '.';
                        }
                    }
                } else if (Regex.IsMatch(item, fallAsleepPattern))
                {
                    MatchCollection matches = Regex.Matches(item, fallAsleepPattern);
                    foreach (Match match in matches)
                    {
                        int h = Int32.Parse(match.Groups[2].Value);
                        int m = Int32.Parse(match.Groups[3].Value);

                        if (h > 0)
                        {
                            currentFallAsleepMinute = 0;
                        }
                        else
                        {
                            currentFallAsleepMinute = m;
                        }

                        for (int i = currentFallAsleepMinute; i < 60; i++)
                        {
                            currentGuard.shifts.Last()[i] = '#';
                        }
                    }
                } else if (Regex.IsMatch(item, wakeUpPattern)) {
                    MatchCollection matches = Regex.Matches(item, wakeUpPattern);
                    foreach (Match match in matches)
                    {
                        int h = Int32.Parse(match.Groups[2].Value);
                        int m = Int32.Parse(match.Groups[3].Value);

                        if (h > 0)
                        {
                            currentWakeUpMinute = 59;
                        }
                        else
                        {
                            currentWakeUpMinute = m;
                        }

                        for (int i = currentWakeUpMinute; i < 60; i++)
                        {
                            currentGuard.shifts.Last()[i] = '.';
                        }
                    }
                }
            }

            int maxTotalMinutesSlept = 0;
            int guardWithMostMinutesSlept = 0;
            int mostMinuteAsleep = 0;

            foreach (Guard g in guards)
            {
                int totalMinutesSlept = g.getTotalMinutesSlept();

                if (totalMinutesSlept > maxTotalMinutesSlept)
                {
                    maxTotalMinutesSlept = totalMinutesSlept;
                    guardWithMostMinutesSlept = g.Id;
                    mostMinuteAsleep = g.getMostMinuteAsleep();
                }

                /*
                foreach (char[] h in g.shifts)
                {
                    foreach (char c in h)
                    {
                        Console.Write(c);
                    }
                    Console.WriteLine();
                }
                */
            }

            int maxOccurencesOnSameMinute = 0;
            int guardWithMostMinutesSlept2 = 0;
            int mostMinuteAsleep2 = 0;
            foreach (Guard f in guards)
            {
                int occurencesOnSameMinute = f.getMaxOccurencesMinute();

                if (occurencesOnSameMinute > maxOccurencesOnSameMinute)
                {
                    maxOccurencesOnSameMinute = occurencesOnSameMinute;
                    guardWithMostMinutesSlept2 = f.Id;
                    mostMinuteAsleep2 = f.getPos();
                }   

            }

            Console.WriteLine("Day4 (1): " + guardWithMostMinutesSlept * mostMinuteAsleep);
            Console.WriteLine("     (2): " + guardWithMostMinutesSlept2 * mostMinuteAsleep2);
        }
    }

    public class Guard
    {
        private int position = 0;
        
        public int Id { get; set; }
        public List<char[]> shifts { get; set; }

        public int pos { get; set; }

        public int getPos()
        {
            return this.position;
        }

        public int getMostMinuteAsleep()
        {
            int mostMinuteAsleep = 0;

            int maxTotalMostMinuteAsleep = 0;

            for (int i = 0; i < 60; i++)
            {
                int totalMostMinuteAsleep = 0;
                foreach (char[] j in shifts)
                {
                    if (j[i] == '#')
                    {
                        totalMostMinuteAsleep++;
                    }
                }

                if (totalMostMinuteAsleep > maxTotalMostMinuteAsleep)
                {
                    maxTotalMostMinuteAsleep = totalMostMinuteAsleep;
                    mostMinuteAsleep = i;
                }
            }

            return mostMinuteAsleep;
        }

        public int getMaxMinutesSlept()
        {
            int maxMinutesSlept = 0;

            foreach (char[] h in shifts)
            {
                int minutesSlept = 0;
                foreach (char c in h)
                {
                    if (c == '#')
                    {
                        minutesSlept++;
                    }
                }

                if (minutesSlept > maxMinutesSlept)
                {
                    maxMinutesSlept = minutesSlept;
                }
            }

            return maxMinutesSlept;
        }

        public int getTotalMinutesSlept()
        {
            int totalMinutes = 0;

            foreach (char[] h in shifts)
            {    
                foreach (char c in h)
                {
                    if (c == '#')
                    {
                        totalMinutes++;
                    }
                }   
            }

            return totalMinutes;
        }

        public int getMaxOccurencesMinute()
        {
            int[] v = new int[60];

            for (int i = 0; i < 60; i++)
            {
                foreach (char[] lastMinutesInShift in shifts)
                {
                    if (lastMinutesInShift[i] == '#')
                    {
                        v[i]++;
                    }
                }
            }

            int maxPos = 0;
            for (int j = 0; j < 60; j++)
            {
                if (v[j] > maxPos)
                {
                    this.position = j;
                    maxPos = v[j];
                }
            }

            return v.Max();
        }

    }
}