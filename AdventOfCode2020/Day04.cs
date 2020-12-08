using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day04
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day04.txt");

            int validPassports2 = 0;


            List<string> passports = new List<string>();
            string tmp = "";
            while ((line = file.ReadLine()) != null)
            {
                tmp = tmp + " " + line;
                if (line.Trim().Length == 0)
                {
                    passports.Add(tmp.Trim());
                    tmp = "";
                }
            }
            if (tmp.Length > 0)
                passports.Add(tmp.Trim());

            int validPassports1 = 0;
            foreach (var passport in passports)
            {
                var dict = passport.Split(' ').Select(x => x.Split(':')).ToDictionary(x => x[0], x => x[1]);
                
                if (dict.ContainsKey("byr") &&
                    dict.ContainsKey("iyr") &&
                    dict.ContainsKey("eyr") &&
                    dict.ContainsKey("hgt") &&
                    dict.ContainsKey("hcl") &&
                    dict.ContainsKey("ecl") &&
                    dict.ContainsKey("pid"))
                {
                    validPassports1++;

                    string output = dict["byr"] + " " + dict["iyr"] + " " + dict["eyr"];

                    int byr = int.Parse(dict["byr"]);
                    if (byr < 1920 || byr > 2002)
                        continue;

                    int iyr = int.Parse(dict["iyr"]);
                    if (iyr < 2010 || iyr > 2020)
                        continue;

                    int eyr = int.Parse(dict["eyr"]);
                    if (eyr < 2020 || eyr > 2030)
                        continue;

                    Regex r = new Regex("([0-9]*)([incm]*)");
                    Match m = r.Match(dict["hgt"]);
                    if (!m.Success)
                    {
                        continue;
                    }
                    else
                    {
                        if (m.Groups[2].Value.Equals("cm"))
                        {
                            int cm = int.Parse(m.Groups[1].Value);
                            if (cm < 150 || cm > 193)
                                continue;
                        }
                        else if (m.Groups[2].Value.Equals("in"))
                        {
                            int inch = int.Parse(m.Groups[1].Value);
                            if (inch < 59 || inch > 76)
                                continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    //output += " " + m.Value;

                    r = new Regex("#[a-f0-9]{6}");
                    m = r.Match(dict["hcl"]);
                    if (!m.Success)
                    {
                        continue;
                    }
                    output += " " + m.Value;

                    r = new Regex("amb|blu|brn|gry|grn|hzl|oth");
                    m = r.Match(dict["ecl"]);
                    output += " " + m.Value;
                    if (!m.Success)
                    {
                        continue;
                    }

                    r = new Regex("[0-9]{9}");
                    m = r.Match(dict["pid"]);
                    output += " " + m.Value;
                    if (!m.Success)
                    {
                        continue;
                    }
                    Console.WriteLine(output);
                    validPassports2++;
                }
            }

            



            Console.WriteLine("Day4 (1): " + validPassports1);
            Console.WriteLine("     (2): " + validPassports2);
        }
    }
}
