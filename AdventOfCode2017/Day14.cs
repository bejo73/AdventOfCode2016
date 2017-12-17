using MoreLinq;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2017
{
    class Day14
    {
        public static void Run()
        {
            int size = 128;
            int[,] grid = new int[size, size];

            string key = "flqrgnkx";
            key = "amgozmfv";
            int used = 0;
            for (int i = 0; i < size; i++)
            {
                string hashStr = PartTwo(key + "-" + i);
                Console.WriteLine("i, " + i +", " + hashStr);

                StringBuilder g = new StringBuilder();
                foreach (char c in hashStr)
                {
                    g.Append(hex2binary(c.ToString()));               
                }

                used = used + g.ToString().Count(x => x == '1');                
            }

            Console.WriteLine("Day14 (1): " + used);
            Console.WriteLine("      (2): ");
        }

        private static string hex2binary(string hexvalue)
        {
            string binaryval = "";
            binaryval = Convert.ToString(Convert.ToInt32(hexvalue, 16), 2);
            return binaryval.PadLeft(4, '0');
        }

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }


        public static string GetKnotHash(string key)
        {
            var input = key
    .Select(c => (int)c)
    .Concat(new[] { 17, 31, 73, 47, 23 })
    .ToList();

            int skip = 0, index = 0, len = 256;
            var list = len.Times();

            foreach (var num in input.Repeat(64))
            {
                list = list.Skip(index).Concat(list.Take(index));
                list = list.Take(num).Reverse().Concat(list.Skip(num));
                list = list.Skip(len - index).Concat(list.Take(len - index));

                index = (num + index + skip) % len;
                ++skip;
            }

            list.BatchesOf(16)
                .Select(batch => batch.Aggregate((x, y) => x ^ y))
                .Select(hash => hash.ToString("x2"))
                .Join("").PrintDump();

            return list.BatchesOf(16)
                .Select(batch => batch.Aggregate((x, y) => x ^ y))
                .Select(hash => hash.ToString("x2"))
                .Join("").ToString();
                //.PrintDump();
        }

        public const int LIST_LENGTH = 256;
        /*
        public static string PartOne(string input)
        {
            var lengths = input.Words().Select(x => int.Parse(x)).ToList();

            var list = Enumerable.Range(0, LIST_LENGTH).ToArray();
            var cur = 0;
            var skip = 0;

            foreach (var length in lengths)
            {
                list = TieKnot(list, cur, skip, length);
                cur += length + skip++;
                cur %= LIST_LENGTH;
            }

            return (list[0] * list[1]).ToString();
        }
        */
        private static int[] TieKnot(int[] list, int cur, int skip, int length)
        {
            var subList = new int[length];

            for (var i = 0; i < length; i++)
            {
                subList[i] = list[(cur + i) % LIST_LENGTH];
            }

            subList = subList.Reverse().ToArray();

            for (var i = 0; i < length; i++)
            {
                list[(cur + i) % LIST_LENGTH] = subList[i];
            }

            return list;
        }

        public static string PartTwo(string input)
        {
            var lengths = input.Select(x => (int)x).ToList();

            lengths = lengths.Concat(new int[] { 17, 31, 73, 47, 23 }).ToList();

            var list = Enumerable.Range(0, LIST_LENGTH).ToArray();
            var cur = 0;
            var skip = 0;

            for (var i = 0; i < 64; i++)
            {
                foreach (var length in lengths)
                {
                    list = TieKnot(list, cur, skip, length);
                    cur += length + skip++;
                    cur %= LIST_LENGTH;
                }
            }

            var denseHash = GetDenseHash(list);

            var result = string.Join(string.Empty, denseHash.Select(x => ConvertToHex(x)));

            return result.ToLower();
        }

        private static string ConvertToHex(int hash)
        {
            return hash.ToString("X").PadLeft(2, '0');
        }

        private static List<int> GetDenseHash(int[] list)
        {
            var denseHash = new List<int>();

            for (var x = 0; x < LIST_LENGTH; x += 16)
            {
                var hash = 0;

                for (int i = 0; i < 16; i++)
                {
                    hash ^= list[x + i];
                }

                denseHash.Add(hash);
            }

            return denseHash;
        }
    }
}