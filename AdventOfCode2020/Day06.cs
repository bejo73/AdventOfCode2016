using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    class Day06
    {

        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day06.txt");
            
            Dictionary<char, int> yesListAnyone = new Dictionary<char, int>();

            int numberOfYesAnyone = 0;
            int numberOfYesEveryone = 0;

            List<string> groups = new List<string>();
            string group = "";
            while ((line = file.ReadLine()) != null)
            {
                group = group + " " + line;
                if (line.Trim().Length == 0)
                {
                    groups.Add(group.Trim());
                    group = "";
                }
            }
            if (group.Length > 0)
                groups.Add(group.Trim());

            foreach (string g in groups)
            {
                string unique = new String(g.Replace(" ", "").Distinct().ToArray());

                foreach (char c in unique.ToCharArray())
                {
                    if (yesListAnyone.ContainsKey(c))
                    {
                        yesListAnyone[c] = yesListAnyone[c] + 1;
                    }
                    else
                    {
                        yesListAnyone.Add(c, 1);
                    }
                }

                List<char> remainingGroupList = new List<char>(unique);

                string[] passengers = g.Split(' ');
                foreach (var p in passengers)
                {
                    List<char> personList = new List<char>(p);

                    var inBothList = remainingGroupList.Intersect(personList);

                    remainingGroupList = inBothList.ToList();
                }

                numberOfYesEveryone += remainingGroupList.Count;
            }

            foreach (var i in yesListAnyone.Values)
            {
                numberOfYesAnyone += i;
            }

            Console.WriteLine("Day6 (1): " + numberOfYesAnyone);
            Console.WriteLine("     (2): " + numberOfYesEveryone);
        }
    }
}