using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    class Day08
    {
        public static void Run()
        {
            int accumulator1 = 0;
            int accumulator2 = 0;
            string line;
            StreamReader file = new StreamReader(@".\Data\Day08.txt");
            List<string> lines = new List<string>();
            List<int> visitedLine = new List<int>();
            List<string> attempts = new List<string>();
            int outRow = 0;

            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }

            // Part 1
            for (int i = 0; i < lines.Count; i++)
            {
                string str = lines[i];
                //Console.WriteLine(i + ", " + str);
                Match m = Regex.Match(lines[i], "(nop|acc|jmp) ([-+]{1})([0-9]*)");
                if (m.Success)
                {
                    string instruction = m.Groups[1].Value;
                    string sign = m.Groups[2].Value;
                    string value = m.Groups[3].Value;

                    if (visitedLine.Contains(i))
                    {
                        break;
                    }
                    visitedLine.Add(i);

                    switch (instruction)
                    {
                        case "nop":
                            break;
                        case "acc":
                            switch (sign)
                            {
                                case "+":
                                    accumulator1 += int.Parse(value);
                                    break;
                                case "-":
                                    accumulator1 -= int.Parse(value);
                                    break;
                            }
                            break;
                        case "jmp":
                            switch (sign)
                            {
                                case "+":
                                    i += int.Parse(value) - 1;
                                    break;
                                case "-":
                                    i -= int.Parse(value) + 1;
                                    break;
                            }
                            break;   
                    }
                }
            }

            // Part 2
            while (outRow + 1 != lines.Count)
            {
                visitedLine = new List<int>();
                accumulator2 = 0;
                bool isChangedInRun = false;

                for (int i = 0; i < lines.Count; i++)
                {
                    string str = lines[i];
                    //Console.WriteLine(i + ", " + str);
                    Match m = Regex.Match(lines[i], "(nop|acc|jmp) ([-+]{1})([0-9]*)");
                    if (m.Success)
                    {
                        string instruction = m.Groups[1].Value;
                        string sign = m.Groups[2].Value;
                        string value = m.Groups[3].Value;

                        if (visitedLine.Contains(i))
                        {
                            break;
                        }
                        visitedLine.Add(i);

                        switch (instruction)
                        {
                            case "nop":
                                if (!attempts.Contains("nop" + i) && !isChangedInRun)
                                {
                                    //Console.WriteLine("nop->jmp");
                                    attempts.Add("nop" + i);

                                    switch (sign)
                                    {
                                        case "+":
                                            i += int.Parse(value) - 1;
                                            break;
                                        case "-":
                                            i -= int.Parse(value) + 1;
                                            break;
                                    }

                                    isChangedInRun = true;
                                    break;
                                }
                                break;

                            case "acc":
                                switch (sign)
                                {
                                    case "+":
                                        accumulator2 += int.Parse(value);
                                        break;
                                    case "-":
                                        accumulator2 -= int.Parse(value);
                                        break;
                                }
                                break;

                            case "jmp":
                                if (!attempts.Contains("jmp" + i) && !isChangedInRun)
                                {
                                    //Console.WriteLine("jmp->nop");
                                    attempts.Add("jmp" + i);
                                    isChangedInRun = true;
                                    break;
                                }

                                switch (sign)
                                {
                                    case "+":
                                        i += int.Parse(value) - 1;
                                        break;
                                    case "-":
                                        i -= int.Parse(value) + 1;
                                        break;
                                }
                                break;
                        }

                        outRow = i;
                    }
                }
            }

            Console.WriteLine("Day8 (1): " + accumulator1);
            Console.WriteLine("     (2): " + accumulator2);
        }
    }
}