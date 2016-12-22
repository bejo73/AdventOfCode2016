using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Twenty
    {
        public static void Run()
        {
            string range;
            StreamReader file = new StreamReader(@".\Data\20.txt");
            List<Range> unorderedRanges = new List<Range>();

            while ((range = file.ReadLine()) != null)
            {
                string[] numbers = range.Split('-');

                long low = Int64.Parse(numbers[0]);
                long high = Int64.Parse(numbers[1]);

                unorderedRanges.Add(new Range() { Low = low, High = high });
            }

            Range lowest = new Range { Low = 0, High = 0 };
            List<Range> orderedRanges = new List<Range>();
            int rangesLeft = unorderedRanges.Count;

            foreach (Range r in unorderedRanges.OrderBy(o => o.Low))
            {
                if (r.Low - 1 > lowest.High)
                {
                    orderedRanges.Add(new Range { Low = lowest.Low, High = lowest.High });
                    lowest.Low = r.Low;
                    lowest.High = r.High;
                }
                else
                {
                    if (r.Low < lowest.Low)
                    {
                        lowest.Low = r.Low;
                    }
                    else
                    {
                        if (r.High > lowest.High)
                        {
                            lowest.High = r.High;
                        }
                    }
                }

                rangesLeft--;
                if (rangesLeft == 0)
                {
                    orderedRanges.Add(new Range { Low = lowest.Low, High = lowest.High });
                }
            }

            Console.WriteLine("First unblocked IP: " + (orderedRanges[0].High + 1));

            // ToDo: Fix this
            // Does not take care of unblocked IP's = 0 and 4294967295

            long numberOfUnblockedIPs = 1;
            long previousHigh = 0;

            foreach (Range current in orderedRanges)
            {
                numberOfUnblockedIPs = numberOfUnblockedIPs + ((current.Low - previousHigh) - 1);
                previousHigh = current.High;
            }

            Console.WriteLine("Number of unblocked ip's: " + numberOfUnblockedIPs);
        }
    }
}