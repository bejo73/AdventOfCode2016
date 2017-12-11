using AdventOfCode2017.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using System.Linq;

namespace AdventOfCode2017
{
    class Day10
    {
        public static void Run()
        {
            //int[] list = { 0, 1, 2, 3, 4 };
            int[] list = new int[256];


            for (int i = 0; i < list.Length; i++)
            {
                list[i] = i;
            }


            //int[] lengths = { 3, 4, 1, 5 };
            int[] lengths = { 120, 93, 0, 90, 5, 80, 129, 74, 1, 165, 204, 255, 254, 2, 50, 113 };

            int currentPostition = 0;
            int skipSize = 0;

            foreach (int length in lengths)
            {
                int[] sub = new int[length];

                if ((length + currentPostition) < list.Length)
                {    
                    Array.Copy(list, currentPostition, sub, 0, length);
                    Array.Reverse(sub);
                    Array.Copy(sub, 0, list, currentPostition, length);
                    currentPostition = currentPostition + length + skipSize;
                    if (currentPostition >= list.Length)
                    {
                        currentPostition = currentPostition - list.Length;

                        if (currentPostition >= list.Length)
                        {
                            currentPostition = currentPostition - list.Length;



                        }
                    }
                    skipSize++;
                }
                else
                {
                    if (length == 255)
                        Console.WriteLine("255");

                    Array.Copy(list, currentPostition, sub, 0, list.Length - currentPostition);
                    Array.Copy(list, 0, sub, list.Length - currentPostition, length - (list.Length - currentPostition));
                    Array.Reverse(sub);
                    Array.Copy(sub, 0, list, currentPostition, list.Length - currentPostition);
                    Array.Copy(sub, list.Length - currentPostition, list, 0, length - (list.Length - currentPostition));

                    currentPostition = currentPostition + length + skipSize;
                    if (currentPostition >= list.Length)
                    {
                        currentPostition = currentPostition - list.Length;

                        if (currentPostition >= list.Length)
                        {
                            currentPostition = currentPostition - list.Length;



                        }

                    }
                    skipSize++;
                }

            }

            Console.WriteLine(list[0]+", "+list[1]);

            Console.WriteLine("Day9 (1): ");
            Console.WriteLine("     (2): ");
        }
    }
}