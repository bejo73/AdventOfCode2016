using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day23_SafeCracking
    {
        public static void Run()
        {
            int reg_a = 7;
            int reg_b = 0;
            int reg_c = 0;
            int reg_d = 0;

            string line;
            StreamReader file = new StreamReader(@".\Data\23.txt");
            List<string> lines = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }

            for (int i = 0; i < lines.Count; i++)
            {
                // Multiply hack
                if (i == 4)
                {
                    reg_a = reg_b * reg_d;
                    reg_c = 0;
                    reg_d = 0;
                    i = 9;
                    continue;
                }

                string str = lines[i];
                //Console.WriteLine(i + ", " + str);
                Match m = Regex.Match(lines[i], "(cpy|inc|dec|jnz|tgl) ([-0-9a-z]*)[ ]{0,1}([-0-9a-z]*)");
                if (m.Success)
                {
                    string instruction = m.Groups[1].Value;
                    string argument1 = m.Groups[2].Value;
                    string argument2 = m.Groups[3].Value;

                    switch (instruction)
                    {
                        case "cpy":

                            int v = 0;

                            if (!Int32.TryParse(argument1, out v))
                            {
                                switch (argument1)
                                {
                                    case "a":
                                        v = reg_a;
                                        break;
                                    case "b":
                                        v = reg_b;
                                        break;
                                    case "c":
                                        v = reg_c;
                                        break;
                                    case "d":
                                        v = reg_d;
                                        break;
                                }
                            }

                            switch (argument2)
                            {
                                case "a":
                                    reg_a = v;
                                    break;
                                case "b":
                                    reg_b = v;
                                    break;
                                case "c":
                                    reg_c = v;
                                    break;
                                case "d":
                                    reg_d = v;
                                    break;
                            }
                            break;
                        case "inc":
                            switch (argument1)
                            {
                                case "a":
                                    reg_a++;
                                    break;
                                case "b":
                                    reg_b++;
                                    break;
                                case "c":
                                    reg_c++;
                                    break;
                                case "d":
                                    reg_d++;
                                    break;
                            }
                            break;
                        case "dec":
                            switch (argument1)
                            {
                                case "a":
                                    reg_a--;
                                    break;
                                case "b":
                                    reg_b--;
                                    break;
                                case "c":
                                    reg_c--;
                                    break;
                                case "d":
                                    reg_d--;
                                    break;
                            }
                            break;

                        case "jnz":
                            bool jump = false;
                            switch (argument1)
                            {
                                case "0":
                                    break;
                                case "a":
                                    if (reg_a != 0) { jump = true; }
                                    break;
                                case "b":
                                    if (reg_b != 0) { jump = true; }
                                    break;
                                case "c":
                                    if (reg_c != 0) { jump = true; }
                                    break;
                                case "d":
                                    if (reg_d != 0) { jump = true; }
                                    break;
                                default:
                                    jump = true;
                                    break;
                            }

                            if (jump)
                            {
                                int steps = 0;

                                if (!Int32.TryParse(argument2, out steps))
                                {
                                    switch (argument2)
                                    {
                                        case "a":
                                            steps = reg_a;
                                            break;
                                        case "b":
                                            steps = reg_b;
                                            break;
                                        case "c":
                                            steps = reg_c;
                                            break;
                                        case "d":
                                            steps = reg_d;
                                            break;
                                    }
                                }

                                if (steps > 0)
                                {
                                    i = i + (steps - 1);
                                }
                                else if (steps < 0)
                                {
                                    i = i + (steps - 1);
                                }
                            }

                            break;
                        case "tgl":
                            int offset = 0;

                            if (!Int32.TryParse(argument1, out offset))
                            {
                                switch (argument1)
                                {
                                    case "a":
                                        offset = reg_a;
                                        break;
                                    case "b":
                                        offset = reg_b;
                                        break;
                                    case "c":
                                        offset = reg_c;
                                        break;
                                    case "d":
                                        offset = reg_d;
                                        break;
                                }
                            }

                            if ((i + offset) < lines.Count)
                            {
                                string toBeToggled = lines[i + offset];

                                string[] splitted = toBeToggled.Split(' ');

                                if (splitted.Length > 2)
                                {
                                    if (splitted[0].Equals("jnz"))
                                    {
                                        lines[i + offset] = "cpy " + splitted[1] + " " + splitted[2];
                                    }
                                    else
                                    {
                                        lines[i + offset] = "jnz " + splitted[1] + " " + splitted[2];
                                    }
                                }
                                else
                                {
                                    if (splitted[0].Equals("inc"))
                                    {
                                        lines[i + offset] = "dec " + splitted[1];
                                    }
                                    else
                                    {
                                        lines[i + offset] = "inc " + splitted[1];
                                    }
                                }
                            }

                            break;
                    }


                }
            }
            Console.WriteLine(reg_a);
        }
    }
}