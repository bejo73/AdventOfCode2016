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
            StreamReader file = new StreamReader(@".\Data\19_Test_1.txt");
            string line;

            List<string> lines = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }

            char[,] diagram = new char[lines[0].Length, lines.Count];

            for (int y = 0; y < lines.Count;  y++)
            {
                string l = lines[y];
                for (int x = 0; x < l.Length; x++)
                {
                    diagram[x, lines.Count - 1 -y] = l[x];
                }
            }

            int currentY = diagram.GetLength(1) - 1; 
            int currentX = 0;

            int empty = 32;

            Console.WriteLine("Start: x=" + currentX + ", y=" + currentY);

            for (int f = 0; f < diagram.GetLength(0); f++)
            {
                if (diagram[f, currentY] == '|')
                {
                    currentX = f;
                    //Console.WriteLine("x: " + currentX + ", y: " + currentY);
                    break;
                }
            }

            Direction direction = Direction.South;

            StringBuilder letters = new StringBuilder();

            int steps = 0;

            while (true)
            {
                Console.WriteLine("x: " + currentX + ", y: " + currentY);
                steps++;
                switch (direction)
                {
                    case Direction.East:
                        currentX++;

                        // East border
                        if (currentX == diagram.GetLength(0))
                        {
                            Console.WriteLine("East border - not implemented");
                            Console.ReadKey();
                        }
                        else
                        {
                            if (diagram[currentX, currentY] == '+')
                            {
                                if (currentY == 0)
                                {
                                    if (diagram[currentX, currentY + 1] != empty)
                                    {
                                        direction = Direction.North;
                                    }
                                    else
                                    {
                                        // Stop
                                    }
                                }
                                else if (currentY == diagram.GetLength(1))
                                {
                                    if (diagram[currentX, currentY - 1] != empty)
                                    {
                                        direction = Direction.South;
                                    }
                                    else
                                    {
                                        // Stop
                                    }
                                }
                                else
                                {
                                    if (diagram[currentX, currentY + 1] != empty)
                                    {
                                        direction = Direction.North;
                                    }
                                    else if (diagram[currentX, currentY - 1] != empty)
                                    {
                                        direction = Direction.South;
                                    }
                                    else
                                    {
                                        // Stop
                                    }

                                    // Continue
                                    if (char.IsLetter(diagram[currentX, currentY]))
                                    {
                                        letters.Append(diagram[currentX, currentY]);
                                    }
                                }
                            }
                            else
                            {
                                if (char.IsLetter(diagram[currentX, currentY]))
                                {
                                    letters.Append(diagram[currentX, currentY]);
                                }

                                // Stop
                                if (diagram[currentX, currentY] == empty)
                                {
                                    Console.WriteLine("East stop: " + letters.ToString());
                                    Console.WriteLine("Steps: " + steps);
                                    Console.ReadKey();
                                }

                            }
                        }
                        break;
                    case Direction.North:
                        currentY++;

                        // North border
                        if (currentY == diagram.GetLength(1))
                        {
                            Console.WriteLine("North border - not implemented");
                            Console.WriteLine(letters.ToString());
                            Console.ReadKey();
                        }
                        else
                        {
                            if (diagram[currentX, currentY] == '+')
                            {
                                if (currentX == 0)
                                {
                                    if (diagram[currentX + 1, currentY] != empty)
                                    {
                                        direction = Direction.East;
                                    }
                                    else
                                    {
                                        // Stop
                                    }
                                }
                                else if (currentX == diagram.GetLength(0))
                                {
                                    if (diagram[currentX - 1, currentY] != empty)
                                    {
                                        direction = Direction.West;
                                    }
                                    else
                                    {
                                        // Stop
                                    }
                                }
                                else
                                {

                                    if (diagram[currentX + 1, currentY] != empty)
                                    {
                                        direction = Direction.East;
                                    }
                                    else if (diagram[currentX - 1, currentY] != empty)
                                    {
                                        direction = Direction.West;
                                    }
                                    else
                                    {
                                        // Stop
                                    }

                                    // Continue
                                    if (char.IsLetter(diagram[currentX, currentY]))
                                    {
                                        letters.Append(diagram[currentX, currentY]);
                                    }
                                }
                            }
                            else
                            {
                                // Continue
                                if (char.IsLetter(diagram[currentX, currentY]))
                                {
                                    letters.Append(diagram[currentX, currentY]);
                                }
                            }
                        }

                        break;
                    case Direction.West:
                        currentX--;

                        // West border
                        if (currentX == 0)
                        {
                            Console.WriteLine("West border - not implemented");
                            Console.WriteLine(letters.ToString());
                            Console.WriteLine("Steps: "+ steps);
                            Console.ReadKey();
                        }
                        else
                        {
                            if (diagram[currentX, currentY] == '+')
                            {
                                if (currentY == 0)
                                {
                                    if (diagram[currentX, currentY + 1] != empty)
                                    {
                                        direction = Direction.North;
                                    }
                                    else
                                    {
                                        // Stop
                                    }
                                }
                                else if (currentY == diagram.GetLength(1))
                                {
                                    if (diagram[currentX, currentY - 1] != empty)
                                    {
                                        direction = Direction.South;
                                    }
                                    else
                                    {
                                        // Stop
                                    }
                                }
                                else
                                {
                                    if (diagram[currentX, currentY + 1] != empty)
                                    {
                                        direction = Direction.North;
                                    }
                                    else if (diagram[currentX, currentY - 1] != empty)
                                    {
                                        direction = Direction.South;
                                    }
                                    else
                                    {
                                        // Stop
                                    }

                                    // Continue
                                    if (char.IsLetter(diagram[currentX, currentY]))
                                    {
                                        letters.Append(diagram[currentX, currentY]);
                                    }
                                }
                            }
                            else
                            {
                                // Continue
                                if (char.IsLetter(diagram[currentX, currentY]))
                                {
                                    letters.Append(diagram[currentX, currentY]);
                                }
                            }
                        }
                        break;

                    case Direction.South:
                        currentY--;

                        // South border
                        if (currentY == 0)
                        {
                            Console.WriteLine("South border");
                            Console.WriteLine(letters.ToString());
                            //Console.ReadKey();

                            if (currentX == 0)
                            {
                                if (diagram[currentX + 1, currentY] != empty)
                                {
                                    direction = Direction.East;
                                }
                                else
                                {
                                    // Stop
                                }
                            }
                            else if (currentX == diagram.GetLength(0))
                            {
                                if (diagram[currentX - 1, currentY] != empty)
                                {
                                    direction = Direction.West;
                                }
                                else
                                {
                                    // Stop
                                }
                            }
                            else
                            {

                                if (diagram[currentX + 1, currentY] != empty)
                                {
                                    direction = Direction.East;
                                }
                                else if (diagram[currentX - 1, currentY] != empty)
                                {
                                    direction = Direction.West;
                                }
                                else
                                {
                                    // Stop
                                }

                                // Continue
                                if (char.IsLetter(diagram[currentX, currentY]))
                                {
                                    letters.Append(diagram[currentX, currentY]);
                                }
                            }




                        }
                        else
                        {
                            if (diagram[currentX, currentY] == '+')
                            {
                                if (currentX == 0)
                                {
                                    if (diagram[currentX + 1, currentY] != empty)
                                    {
                                        direction = Direction.East;
                                    }
                                    else
                                    {
                                        // Stop
                                    }
                                }
                                else if (currentX == diagram.GetLength(0))
                                {
                                    if (diagram[currentX - 1, currentY] != empty)
                                    {
                                        direction = Direction.West;
                                    }
                                    else
                                    {
                                        // Stop
                                    }
                                }
                                else
                                {

                                    if (diagram[currentX + 1, currentY] != empty)
                                    {
                                        direction = Direction.East;
                                    }
                                    else if (diagram[currentX - 1, currentY] != empty)
                                    {
                                        direction = Direction.West;
                                    }
                                    else
                                    {
                                        // Stop
                                    }

                                    // Continue
                                    if (char.IsLetter(diagram[currentX, currentY]))
                                    {
                                        letters.Append(diagram[currentX, currentY]);
                                    }
                                }
                            }
                            else
                            {
                                // Continue
                                if (char.IsLetter(diagram[currentX, currentY]))
                                {
                                    letters.Append(diagram[currentX, currentY]);
                                }
                            }
                        }


                        break;
                }

            }

            Console.WriteLine("Day 3 (1): ");
            Console.WriteLine("      (2): ");
        }
    }
}