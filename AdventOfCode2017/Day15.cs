using System;

namespace AdventOfCode2017
{
    class Day15
    {
        public static void Run()
        {
            int aStart = 618; // 65;
            int bStart = 814; // 8921;

            int aFactor = 16807;
            int bFactor = 48271;

            int divider = 2147483647;

            // Part I
            long genA = aStart;
            long genB = bStart;
            int counter = 0;
            int match = 0;

            while (true)
            {
                genA = (genA * aFactor) % divider;
                genB = (genB * bFactor) % divider;

                if ((GetLSB(genA) ^ GetLSB(genB)) == 0)
                    match++;

                counter++;

                if (counter >= 40000000)
                    break;
            }

            Console.WriteLine("Day15 (1): " + match);

            // Part II
            genA = aStart;
            genB = bStart;
            counter = 0;
            match = 0;

            bool aFound = false;
            bool bFound = false;
            int pair = 0;

            while (true)
            {
                if (!aFound)
                {
                    genA = (genA * aFactor) % divider;

                    if (genA % 4 == 0)
                    {
                        aFound = true;
                    }
                }

                if (!bFound)
                {
                    genB = (genB * bFactor) % divider;

                    if (genB % 8 == 0)
                    {
                        bFound = true;
                    }
                }

                if (aFound && bFound)
                {
                    if ((GetLSB(genA) ^ GetLSB(genB)) == 0)
                    {
                        match++;
                    }

                    pair++;
                    aFound = false;
                    bFound = false;
                }

                if (pair >= 5000000)
                    break;
            }
            
            Console.WriteLine("      (2): " + match);
        }

        public static long GetLSB(long intValue)
        {
            return (intValue & 0x000000000000FFFF);
        }
    }
}