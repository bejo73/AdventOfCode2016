using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Eight
    {
        public static void Run()
        {
            const int X = 50, Y = 6;

            string line;
            StreamReader file = new StreamReader(@".\Data\8.txt");

            int[,] screen = new int[X, Y];

            while ((line = file.ReadLine()) != null)
            {
                Match m = Regex.Match(line, "rect ([0-9]*)x([0-9]*)");
                if (m.Success)
                {
                    int x = Int32.Parse(m.Groups[1].Value);
                    int y = Int32.Parse(m.Groups[2].Value);

                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y; j++)
                        {
                            screen[i, j] = 1;
                        }
                    }
                    continue;
                }

                m = Regex.Match(line, "rotate row y=([0-9]*) by ([0-9]*)");
                if (m.Success)
                {
                    int y = Int32.Parse(m.Groups[1].Value);
                    int shift = Int32.Parse(m.Groups[2].Value);

                    int[] t = new int[shift];
                    for (int i = 0; i < shift; i++)
                    {
                        t[i] = screen[(X-shift) + i, y];
                    }

                    for (int j = (X - 1); j >= shift; j--)
                    {
                        screen[j, y] = screen[j - shift, y];
                    }

                    for (int j = 0 ; j < shift; j++)
                    {
                        screen[j , y] = t[j];
                    }

                    continue;
                }

                m = Regex.Match(line, "rotate column x=([0-9]*) by ([0-9]*)");
                if (m.Success)
                {
                    int x = Int32.Parse(m.Groups[1].Value);
                    int shift = Int32.Parse(m.Groups[2].Value);

                    int[] t = new int[6];
                    for (int i = 0; i < 6; i++)
                    {
                        t[i] = screen[x, i];
                    }

                    for (int j = 0; j < 6 - shift; j++)
                    {
                        screen[x, j + shift] = t[j];
                    }

                    for (int j = 6 - shift; j < 6; j++)
                    {
                        screen[x, j - 6 + shift] = t[j];
                    }

                    continue;
                }
            }

            int c = 0;

            for (int i = 0; i < screen.GetLength(0); i++)
            {
                for (int j = 0; j < screen.GetLength(1); j++)
                {
                    Console.WriteLine("x={0}, y={1} => {2}", i, j, screen[i, j]);

                    if (screen[i, j] == 1)
                    {
                        c++;
                    }
                }
            }

            for (int j = 0; j < screen.GetLength(1); j++)
            {
                for (int i = 0; i < screen.GetLength(0); i++)
                {
                    if (screen[i, j] == 0)
                    {
                        Console.Write(".");
                    }
                    else
                        Console.Write("#");
                }
                Console.WriteLine();
            }

            Console.Write(c);
        }
    }
}