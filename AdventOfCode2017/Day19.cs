using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2017
{
    class Day19
    {
        enum Direction { North, South, West, East };

        public static void Run()
        {
            StreamReader file = new StreamReader(@".\Data\19.txt");
            string line;

            List<string> lines = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }

            char[,] diagram = new char[lines[0].Length, lines.Count];

            for (int y = 0; y < lines.Count; y++)
            {
                string l = lines[y];
                for (int x = 0; x < l.Length; x++)
                {
                    diagram[x, lines.Count - 1 - y] = l[x];
                }
            }

            int currentY = diagram.GetLength(1) - 1;
            int currentX = 0;

            int empty = 32;

            // Find starting x position
            for (int f = 0; f < diagram.GetLength(0); f++)
            {
                if (diagram[f, currentY] == '|')
                {
                    currentX = f;
                    break;
                }
            }

            Direction direction = Direction.South;

            StringBuilder letters = new StringBuilder();

            int steps = 0;

            bool ready = false;
            while (!ready)
            {
                //Console.WriteLine("x: " + currentX + ", y: " + currentY);
                steps++;

                switch (direction)
                {
                    case Direction.East:
                        currentX++;

                        if (diagram[currentX, currentY] == '+')
                        {
                            if (diagram[currentX, currentY + 1] != empty)
                            {
                                direction = Direction.North;
                            }
                            else if (diagram[currentX, currentY - 1] != empty)
                            {
                                direction = Direction.South;
                            }
                        }

                        break;
                    case Direction.North:
                        currentY++;

                        if (diagram[currentX, currentY] == '+')
                        {
                            if (diagram[currentX + 1, currentY] != empty)
                            {
                                direction = Direction.East;
                            }
                            else if (diagram[currentX - 1, currentY] != empty)
                            {
                                direction = Direction.West;
                            }
                        }


                        break;
                    case Direction.West:
                        currentX--;

                        if (diagram[currentX, currentY] == '+')
                        {
                            if (diagram[currentX, currentY + 1] != empty)
                            {
                                direction = Direction.North;
                            }
                            else if (diagram[currentX, currentY - 1] != empty)
                            {
                                direction = Direction.South;
                            }
                        }

                        break;

                    case Direction.South:
                        currentY--;

                        if (diagram[currentX, currentY] == '+')
                        {
                            if (diagram[currentX + 1, currentY] != empty)
                            {
                                direction = Direction.East;
                            }
                            else if (diagram[currentX - 1, currentY] != empty)
                            {
                                direction = Direction.West;
                            }
                        }

                        break;
                }

                if (char.IsLetter(diagram[currentX, currentY]))
                {
                    letters.Append(diagram[currentX, currentY]);
                }

                // Stop
                if (diagram[currentX, currentY] == empty)
                {
                    ready = true;
                }
            }

            Console.WriteLine("Day 19 (1): " + letters.ToString());
            Console.WriteLine("       (2): " + steps);
        }
    }
}