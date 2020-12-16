using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    class Day12
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day12.txt");

            char direction = 'E';
            int x = 0, x2 = 0;
            int y = 0, y2 = 0;

            int wayPointX = 10;
            int wayPointY = 1;

            int oldX = 0;
            int oldY = 0;

            while ((line = file.ReadLine()) != null)
            {
                char action = line[0];
                int value = int.Parse(line.Substring(1));

                switch (action)
                {
                    case 'N':
                        y += value;
                        wayPointY += value; ;
                        break;
                    case 'S':
                        y -= value;
                        wayPointY -= value; 
                        break;
                    case 'E':
                        x += value;
                        wayPointX += value; ;
                        break;
                    case 'W':
                        x -= value;
                        wayPointX -= value; ;
                        break;
                    case 'L':
                        switch (value)
                        {
                            case 90:
                                switch (direction)
                                {
                                    case 'N':
                                        direction = 'W';
                                        break;
                                    case 'S':
                                        direction = 'E';
                                        break;
                                    case 'E':
                                        direction = 'N';
                                        break;
                                    case 'W':
                                        direction = 'S';
                                        break;
                                    default:
                                        break;
                                }

                                oldX = wayPointX;
                                wayPointX = -wayPointY;
                                wayPointY = oldX;

                                break;
                            case 180:
                                switch (direction)
                                {
                                    case 'N':
                                        direction = 'S';
                                        break;
                                    case 'S':
                                        direction = 'N';
                                        break;
                                    case 'E':
                                        direction = 'W';
                                        break;
                                    case 'W':
                                        direction = 'E';
                                        break;
                                    default:
                                        break;
                                }
                                wayPointX = -wayPointX;
                                wayPointY = -wayPointY;
                                break;
                            case 270:
                                switch (direction)
                                {
                                    case 'N':
                                        direction = 'E';
                                        break;
                                    case 'S':
                                        direction = 'W';
                                        break;
                                    case 'E':
                                        direction = 'S';
                                        break;
                                    case 'W':
                                        direction = 'N';
                                        break;
                                    default:
                                        break;
                                }

                                oldX = wayPointX;
                                oldY = wayPointY;
                                wayPointY = -wayPointX;

                                wayPointX = oldY;

                                break;
                            default:
                                break;
                        }
                        break;
                    case 'R':
                        switch (value)
                        {
                            case 90:
                                switch (direction)
                                {
                                    case 'N':
                                        direction = 'E';
                                        break;
                                    case 'S':
                                        direction = 'W';
                                        break;
                                    case 'E':
                                        direction = 'S';
                                        break;
                                    case 'W':
                                        direction = 'N';
                                        break;
                                    default:
                                        break;
                                }

                                oldX = wayPointX;
                                oldY = wayPointY;
                                wayPointX = wayPointY;
                                wayPointY = -oldX;

                                break;
                            case 180:
                                switch (direction)
                                {
                                    case 'N':
                                        direction = 'S';
                                        break;
                                    case 'S':
                                        direction = 'N';
                                        break;
                                    case 'E':
                                        direction = 'W';
                                        break;
                                    case 'W':
                                        direction = 'E';
                                        break;
                                    default:
                                        break;
                                }



                                wayPointX = -wayPointX;
                                wayPointY = -wayPointY;

                                break;
                            case 270:
                                switch (direction)
                                {
                                    case 'N':
                                        direction = 'W';
                                        break;
                                    case 'S':
                                        direction = 'E';
                                        break;
                                    case 'E':
                                        direction = 'N';
                                        break;
                                    case 'W':
                                        direction = 'S';
                                        break;
                                    default:
                                        break;
                                }


                                oldX = wayPointX;
                                oldY = wayPointY;
                                wayPointX = -wayPointY;
                                wayPointY = oldX;



                                break;
                            default:
                                break;
                        }
                        break;
                    case 'F':
                        switch (direction)
                        {
                            case 'N':
                                y = y + value;
                                break;
                            case 'S':
                                y = y - value;
                                break;
                            case 'E':
                                x = x + value;
                                break;
                            case 'W':
                                x = x - value;
                                break;
                            default:
                                break;
                        }

                        x2 = x2 + value * (wayPointX);
                        

                        y2 = y2 + value * (wayPointY);
                        

                        break;
                    default:
                        break;
                }
            }

            

            Console.WriteLine("Day10 (1): " + "x="+Math.Abs(x)+",y="+Math.Abs(y)+"="+(Math.Abs(x)+Math.Abs(y)));
            Console.WriteLine("      (2): " + "x=" + Math.Abs(x2) + ",y=" + Math.Abs(y2) + "=" + (Math.Abs(x2) + Math.Abs(y2)));
        }
    }
}