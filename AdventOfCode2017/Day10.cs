using AdventOfCode2017.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using System.Linq;

using System.Text;


using MoreLinq;
using ServiceStack;
using ServiceStack.Text;


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

            Console.WriteLine("Day9 (1): " + KnotHashPartOne("120, 93, 0, 90, 5, 80, 129, 74, 1, 165, 204, 255, 254, 2, 50, 113"));
           // Console.WriteLine("     (2): " + KnotHashPartTwo("120, 93, 0, 90, 5, 80, 129, 74, 1, 165, 204, 255, 254, 2, 50, 113"));



        }


       


        static int KnotHashPartOne(string input)
        {
            var sparseHash = input
            .Split(',')
            .Select(i => byte.Parse(i))
            .ToSparseHashSequence(1)
                .Last();
            return sparseHash[0] * sparseHash[1];
        }

        
        
        /*



        static string  KnotHashPartTwo(string input)
        {
            var sparseHash = input
                .ToCharArray()
                .Select(i => (byte)i)
                .Concat(new byte[] { 0x11, 0x1f, 0x49, 0x2f, 0x17 })
                .ToSparseHashSequence(64)
                .Last();

            var denseHash = sparseHash
        .Select((v, i) => (value: v, index: i))
        .GroupBy(i => i.index / 16)
        .Select(g => g.Aggregate(0x0, (acc, i) => (byte)(acc ^ i.value)));


            return denseHash
                .Aggregate(new StringBuilder(), (acc, i) => acc.Append($"{i:x2}"))
                .ToString();
        }
        
    */
    }

    static class Extensions
    {
        public static IEnumerable<byte[]> ToSparseHashSequence(this IEnumerable<byte> lengths, int repeat)
        {
            var size = 256;
            var position = 0;
            var skip = 0;
            var state = Enumerable.Range(0, size).Select(i => (byte)i).ToArray();
            yield return state;
            for (var _ = 0; _ < repeat; _++)
            {
                foreach (var length in lengths)
                {
                    if (length > 1) state = state.Select((v, i) => ((i < position && i + size >= position + length) || i >= position + length) ? v : state[(2 * position + length + size - i - 1) % size]).ToArray();
                    yield return state;
                    position = (position + length + skip++) % size;
                }
            }
        }
    }
}

