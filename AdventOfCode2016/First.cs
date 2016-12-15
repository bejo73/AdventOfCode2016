using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Coordinate
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coordinate() { }

        public string getUnique()
        {
            return "" + x + y;
        }
    }

    class First
    {
        enum Direction { North, South, West, East };

        public static void Run()
        {
            string input = "R5, L2, L1, R1, R3, R3, L3, R3, R4, L2, R4, L4, R4, R3, L2, L1, L1, R2, R4, R4, L4, R3, L2, R1, L4, R1, R3, L5, L4, L5, R3, L3, L1, L1, R4, R2, R2, L1, L4, R191, R5, L2, R46, R3, L1, R74, L2, R2, R187, R3, R4, R1, L4, L4, L2, R4, L5, R4, R3, L2, L1, R3, R3, R3, R1, R1, L4, R4, R1, R5, R2, R1, R3, L4, L2, L2, R1, L3, R1, R3, L5, L3, R5, R3, R4, L1, R3, R2, R1, R2, L4, L1, L1, R3, L3, R4, L2, L4, L5, L5, L4, R2, R5, L4, R4, L2, R3, L4, L3, L5, R5, L4, L2, R3, R5, R5, L1, L4, R3, L1, R2, L5, L1, R4, L1, R5, R1, L4, L4, L4, R4, R3, L5, R1, L3, R4, R3, L2, L1, R1, R2, R2, R2, L1, L1, L2, L5, L3, L1";
            string[] commands = input.Split(',');

            List<Coordinate> co = new List<Coordinate>();

            int north = 0;
            int south = 0;
            int west = 0;
            int east = 0;

            Coordinate current = new Coordinate() { x = 0, y = 0 };
            Direction direction = Direction.North;

            HashSet<string> hash = new HashSet<string>();
            hash.Add(current.getUnique());

            bool beenHereBefore = false;

            foreach (string command in commands)
            {
                string c = command.Trim();

                int length = int.Parse(c.Substring(1));

                switch (direction)
                {
                    case Direction.North:
                        if (c.StartsWith("R"))
                        {
                            direction = Direction.East;
                            east = east + length;

                            for (int i = 0; i < length; i++)
                            {
                                current.x++;
                                beenHereBefore = BeenHereBefore(beenHereBefore, current, hash);
                            }
                        }
                        else
                        {
                            direction = Direction.West;
                            west = west + length;

                            for (int i = 0; i < length; i++)
                            {
                                current.x--;
                                beenHereBefore = BeenHereBefore(beenHereBefore, current, hash);
                            }
                        }
                        break;
                    case Direction.South:
                        if (c.StartsWith("R"))
                        {
                            direction = Direction.West;
                            west = west + length;

                            for (int i = 0; i < length; i++)
                            {
                                current.x--;
                                beenHereBefore = BeenHereBefore(beenHereBefore, current, hash);
                            }
                        }
                        else
                        {
                            direction = Direction.East;
                            east = east + length;

                            for (int i = 0; i < length; i++)
                            {
                                current.x++;
                                beenHereBefore = BeenHereBefore(beenHereBefore, current, hash);
                            }
                        }
                        break;
                    case Direction.West:
                        if (c.StartsWith("R"))
                        {
                            direction = Direction.North;
                            north = north + length;

                            for (int i = 0; i < length; i++)
                            {
                                current.y++;
                                beenHereBefore = BeenHereBefore(beenHereBefore, current, hash);
                            }
                        }
                        else
                        {
                            direction = Direction.South;
                            south = south + length;

                            for (int i = 0; i < length; i++)
                            {
                                current.y--;
                                beenHereBefore = BeenHereBefore(beenHereBefore, current, hash);
                            }
                        }
                        break;
                    case Direction.East:
                        if (c.StartsWith("R"))
                        {
                            direction = Direction.South;
                            south = south + length;

                            for (int i = 0; i < length; i++)
                            {
                                current.y--;
                                beenHereBefore = BeenHereBefore(beenHereBefore, current, hash);
                            }
                        }
                        else
                        {
                            direction = Direction.North;
                            north = north + length;

                            for (int i = 0; i < length; i++)
                            {
                                current.y++;
                                beenHereBefore = BeenHereBefore(beenHereBefore, current, hash);
                            }
                        }
                        break;
                }
            }

            int ay = Math.Abs(north - south);
            int ax = Math.Abs(east - west);

            Console.WriteLine("Distance A: " + (ay + ax));
        }

        private static bool BeenHereBefore(bool beenHereBefore, Coordinate current, HashSet<string> hash)
        {
            if (!hash.Add(current.getUnique()) && !beenHereBefore)
            {
                Console.WriteLine("Been here before, x: " + current.x + ", y: " + current.y);
                Console.WriteLine("Distance B: " + (Math.Abs(current.x) + Math.Abs(current.y)));
                beenHereBefore = true;
            }
            return beenHereBefore;
        }
    }
}