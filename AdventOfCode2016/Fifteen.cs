﻿using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Fifteen
    {
        public static void Run()
        {
            List<Disc> discs = new List<Disc>();

            // Test
            //discs.Add(new Disc(0, 5));
            //discs.Add(new Disc(1, 2));

            /*  A
                Disc #1 has 5 positions; at time=0, it is at position 2.
                Disc #2 has 13 positions; at time=0, it is at position 7.
                Disc #3 has 17 positions; at time=0, it is at position 10.
                Disc #4 has 3 positions; at time=0, it is at position 2.
                Disc #5 has 19 positions; at time=0, it is at position 9.
                Disc #6 has 7 positions; at time=0, it is at position 0.

                B
                Disc #7 has 11 positions; at time=0, it is at position 0.
            */

            discs.Add(new Disc(2, 5));
            discs.Add(new Disc(7, 13));
            discs.Add(new Disc(10,17));
            discs.Add(new Disc(2, 3));
            discs.Add(new Disc(9, 19));
            discs.Add(new Disc(0, 7));
            discs.Add(new Disc(0, 11));

            int time = 0;

            while (true)
            {
                bool allZero = true;
                int offset = 1;
                foreach (Disc d in discs)
                {                    
                    if (!d.IsZero(time + offset))
                    {
                        allZero = false;
                    }
                    offset++;
                }

                if (allZero)
                {
                    Console.WriteLine(time);
                    break;
                }
                time++;
            }
        }
    }
}