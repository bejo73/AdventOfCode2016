using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Eight
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\Eight.txt");

            List<string> strings = new List<string>();

            int[,] screen = new int[50, 6];

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

                    int[] t = new int[50];
                    for (int i = 0; i < 50; i++)
                    {
                        t[i] = screen[i, y];
                    }

                    for (int j = 0; j < 50 - shift; j++)
                    {
                        screen[j + shift, y] = t[j];
                    }

                    for (int j = 50 - shift; j < 50; j++)
                    {
                        screen[j - 50 + shift, y] = t[j];
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

                    //Console.Write(screen[i, j]);

                    if (screen[i, j] == 1)
                        c++;

                    //screen[i, j] = i * 3 + j;
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