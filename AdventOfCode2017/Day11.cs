using System;
using System.IO;

namespace AdventOfCode2017
{
    class Day11
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\11.txt");

            // Start
            int y = 0;
            int x = 0;
            Hexagon a = new Hexagon(x, y);
            Hexagon b = null;

            int max = 0;

            while ((line = file.ReadLine()) != null)
            {
                string[] steps = line.Split(',');

                foreach (string step in steps)
                {
                    switch (step.Trim())
                    {
                        case "n":
                            y--;
                            break;
                        case "s":
                            y++;
                            break;
                        case "ne":
                            y--;
                            x++;
                            break;
                        case "nw":
                            x--;
                            break;
                        case "se":
                            x++;
                            break;
                        case "sw":
                            y++;
                            x--;
                            break;
                    }


                    b = new Hexagon(x, y);
                    int distance = hex_distance(a, b);

                    if (distance > max)
                    {
                        max = distance;
                    }
                }

                Console.WriteLine("y: " + y);
                Console.WriteLine("x: " + x);
            }
  

            Console.WriteLine("Day11 (1): " + hex_distance(a, b));
            Console.WriteLine("      (2): " + max);
        }

        // Hexagon theory inspired by: https://www.redblobgames.com/grids/hexagons/

        private static Hexagon hex_subtract(Hexagon a, Hexagon b)
        {
            return new Hexagon(a.q - b.q, a.r - b.r, a.s - b.s);
        }

        private static int hex_length(Hexagon hex)
        {

            return Convert.ToInt32((Math.Abs(hex.q) + Math.Abs(hex.r) + Math.Abs(hex.s)) / 2);
        }

        private static int hex_distance(Hexagon a, Hexagon b)
        {
            return hex_length(hex_subtract(a, b));
        }
    }

    class Hexagon
    {
        public int q;
        public int r;
        public int s;

        public Hexagon(int q, int r)
        {
            this.q = q;
            this.r = r;
            s = 0 - q - r;    
        }

        public Hexagon(int q, int r, int s)
        {
            this.q = q;
            this.r = r;
            this.s = s;
        }
    }
}