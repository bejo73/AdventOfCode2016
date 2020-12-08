using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day07
    {
        static int bagCounter = 0;

        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day07.txt");
            
            int part1 = 0;
            int part2 = 0;

            List<Bag> bagList = new List<Bag>();

            while ((line = file.ReadLine()) != null)
            {
                //Console.WriteLine(line);
                string currentBag = line.Substring(0, line.IndexOf("s contain"));

                var bag = new Bag()
                {
                    Type = currentBag,
                    Quantity = 1
                };

                string[] includedBags = line.Substring(line.IndexOf("s contain") + 9).Split(',');

                if (!includedBags[0].Contains(" no other"))
                {
                    List<Bag> includedBagsList = new List<Bag>();

                    foreach (var ib in includedBags)
                    {
                        Regex r = new Regex("([0-9]*) ([ a-zA-Z]*)");
                        Match m = r.Match(ib.Trim());
                        if (m.Success)
                        {
                            int numberOfBags = int.Parse(m.Groups[1].Value);
                            string type = m.Groups[2].Value.Replace("bags", "bag");
                            includedBagsList.Add(new Bag() { Type = type, Quantity = numberOfBags });
                        }
                    }

                    bag.Bags = includedBagsList;
                }

                bagList.Add(bag);
            }

            foreach (var b in bagList)
            {
                if (ContainsGold(b, bagList))
                    part1++;
            }

            CountBags(bagList.FirstOrDefault(i => i.Type == "shiny gold bag"), bagList, 1);
            part2 = bagCounter;

            Console.WriteLine("Day7 (1): " + part1);
            Console.WriteLine("     (2): " + part2);
        }

        public static bool ContainsGold(Bag bag, List<Bag> bagList)
        {
            if (bag.Bags == null)
                return false;

            foreach (var b in bag.Bags)
            {
                if (b.Type.Equals("shiny gold bag"))
                    return true;

                Bag bagObj = bagList.FirstOrDefault(i => i.Type == b.Type);

                if (ContainsGold(bagObj, bagList))
                    return true;                
            }

            return false;
        }

        public static void CountBags(Bag bag, List<Bag> bagList, int q)
        {
            if (bag.Bags == null)
                return;

            foreach (var b in bag.Bags)
            {
                bagCounter += b.Quantity * q;

                Bag bagObj = bagList.FirstOrDefault(i => i.Type == b.Type);

                CountBags(bagObj, bagList, b.Quantity * q);            
            }

            return;  
        }
    }

    public class Bag
    {
        public string Type { get; set; }

        public int Quantity { get; set; }

        public List<Bag> Bags { get; set; }
    }
}