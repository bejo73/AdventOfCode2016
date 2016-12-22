using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class TwentyOne
    {
        public static void Run()
        {
            string line;

            // Test
            //StreamReader file = new StreamReader(@".\Data\21_Test.txt");
            //string input = "abcde";

            StreamReader file = new StreamReader(@".\Data\21.txt");
            string input = "abcdefgh";

            int counter = 0;

            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(counter + ", " + input);
                counter++;

                Match m = Regex.Match(line, "swap position ([0-9]*) with position ([0-9]*)");
                if (m.Success)
                {
                    int p1 = Int32.Parse(m.Groups[1].Value);
                    int p2 = Int32.Parse(m.Groups[2].Value);
                    string s1 = input.Substring(p1, 1);
                    string s2 = input.Substring(p2, 1);
                    input = input.Remove(p1, 1).Insert(p1, s2);
                    input = input.Remove(p2, 1).Insert(p2, s1);
                    continue;
                }
                m = Regex.Match(line, "swap letter ([a-z]*) with letter ([a-z]*)");
                if (m.Success)
                {
                    string l1 = m.Groups[1].Value;
                    string l2 = m.Groups[2].Value;
                    int i1 = input.IndexOf(l1);
                    int i2 = input.IndexOf(l2);
                    input = input.Remove(i1, 1).Insert(i1, l2);
                    input = input.Remove(i2, 1).Insert(i2, l1);
                    continue;
                }
                m = Regex.Match(line, "reverse positions ([0-9]*) through ([0-9]*)");
                if (m.Success)
                {
                    int p1 = Int32.Parse(m.Groups[1].Value);
                    int p2 = Int32.Parse(m.Groups[2].Value);
                    char[] helper = input.Substring(p1, p2 - p1 + 1).ToCharArray();
                    Array.Reverse(helper);
                    string reversed = new string(helper);
                    input = input.Substring(0, p1) + reversed + input.Substring(p2 + 1);
                    continue;
                }
                m = Regex.Match(line, "rotate (right|left) ([0-9]*)");
                if (m.Success)
                {
                    string direction = m.Groups[1].Value;
                    int pos = Int32.Parse(m.Groups[2].Value);
                    if (direction.Equals("right"))
                    {
                        input = input.Substring(input.Length - pos) + input.Substring(0, input.Length - pos);
                    }
                    else
                    {
                        input = input.Substring(pos) + input.Substring(0, pos);
                    }
                    continue;
                }
                m = Regex.Match(line, "rotate based on position of letter ([a-z]*)");
                if (m.Success)
                {
                    string letter = m.Groups[1].Value;
                    int pos = input.IndexOf(letter);
                    int shift = 1 + pos;

                    if (shift >= 5)
                    {
                        shift++;
                    }
                    if (shift > input.Length)
                    {
                        shift = shift - input.Length;
                        input = input.Substring(input.Length - shift) + input.Substring(0, input.Length - shift);
                    }
                    else
                    {
                        input = input.Substring(input.Length - shift) + input.Substring(0, input.Length - shift );
                    }
                    continue;
                }
                m = Regex.Match(line, "move position ([0-9]*) to position ([0-9]*)");
                if (m.Success)
                {
                    int p1 = Int32.Parse(m.Groups[1].Value);
                    int p2 = Int32.Parse(m.Groups[2].Value);
                    string s1 = input.Substring(p1, 1);
                    
                    if (p1 > p2)
                    {
                        input = input.Remove(p1, 1);
                        input = input.Insert(p2, s1);
                    }
                    else
                    {
                        input = input.Remove(p1, 1);
                        input = input.Insert(p2, s1);
                        
                    }
                    continue;
                }
            }
            Console.WriteLine(counter + ", " + input);
            counter++;
        }
    }
}