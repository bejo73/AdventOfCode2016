using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2017
{
    class Day12
    {
        private static List<Address> addresses = new List<Address>();
        private static HashSet<int> connected = new HashSet<int>();

        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\12.txt");

            while ((line = file.ReadLine()) != null)
            {
                string pattern = @"([0-9]*) <-> ([, 0-9]*)";

                MatchCollection matches = Regex.Matches(line, pattern);

                foreach (Match match in matches)
                {
                    string addressStr = match.Groups[1].Value;
                    string neighboursStr = match.Groups[2].Value;

                    string[] split = neighboursStr.Split(',');

                    int[] ids = new int[split.Length];
                    for (int i = 0; i < split.Length; i++)
                    {
                        ids[i] = Int32.Parse(split[i]);
                    }

                    addresses.Add(new Address(Int32.Parse(addressStr), ids));
                }
            }

            int groups = 0;
            int connectedToZero = 0;

            foreach (Address a in addresses)
            {
                if (!connected.Any(c => c == a.id))
                {
                    Follow(a);
                    groups++;
                }

                if (groups == 1)
                {
                    connectedToZero = connected.Count;
                }
            }

            Console.WriteLine("Day12 (1): " + connectedToZero);
            Console.WriteLine("      (2): " + groups);
        }

        private static void Follow(Address a)
        {
            if (connected.Add(a.id))
            {
                foreach (int i in a.neighbourIds)
                {
                    Address address = addresses.FirstOrDefault(adr => adr.id == i);
                    Follow(address);
                }
            }
        }
    }

    class Address
    {
        public int id;
        public int[] neighbourIds;

        public Address(int id, int[] neighbourIds)
        {
            this.id = id;
            this.neighbourIds = neighbourIds;
        }
    }

}