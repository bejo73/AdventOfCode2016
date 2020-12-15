using System;
using System.IO;

namespace AdventOfCode2020
{
    class Day11
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day11.txt");

            //int COLUMNS = 10;
            int COLUMNS = 95;
            //int ROWS = 10;
            int ROWS = 91;

            int numberOfOccupiedSeats1 = 0;
            int numberOfOccupiedSeats2 = 0;

            char[,] seats1 = new char[ROWS, COLUMNS];
            char[,] seats2 = new char[ROWS, COLUMNS];

            int x = 0;
            int y = 0;

            while ((line = file.ReadLine()) != null)
            {
                foreach (char c in line.Trim().ToCharArray())
                {
                    seats1[y, x] = c;
                    seats2[y, x++] = c;
                }
                x = 0;
                y++;
            }

            bool changed = true;
            while (changed)
            {
                changed = false;
                char[,] tmpSeats = new char[ROWS, COLUMNS];

                for (int row = 0; row < ROWS; row++)
                {
                    for (int col = 0; col < COLUMNS; col++)
                    {
                        int xStart = col - 1;
                        int xLength = 3;
                        int yStart = row - 1;
                        int yLength = 3;

                        if (col == 0)
                        {
                            xStart = 0;
                            xLength = 2;
                        }
                        else if (col == COLUMNS - 1)
                        {
                            xLength = 2;
                        }
                        if (row == 0)
                        {
                            yStart = 0;
                            yLength = 2;
                        }
                        else if (row == ROWS -1)
                        {
                            yLength = 2;
                        }

                        int occupied = 0;

                        switch (seats1[row, col])
                        {
                            case '#':
                                for (int a = xStart; a < xStart + xLength; a++)
                                {
                                    for (int b = yStart; b < yStart + yLength; b++)
                                    {
                                        if (seats1[b, a] == '#')
                                        {
                                            occupied++;
                                        }
                                    }
                                }
                                if (occupied > 4)
                                {
                                    tmpSeats[row, col] = 'L';
                                    changed = true;
                                }
                                else
                                {
                                    tmpSeats[row, col] = '#';
                                }
                                break;
                            case 'L':
                                occupied = 0;
                                for (int a = xStart; a < xStart + xLength; a++)
                                {
                                    for (int b = yStart; b < yStart + yLength; b++)
                                    {
                                        if (seats1[b, a] == '#')
                                        {
                                            occupied++;
                                        }
                                    }
                                }
                                if (occupied == 0)
                                {
                                    tmpSeats[row, col] = '#';
                                    changed = true;
                                }
                                else
                                {
                                    tmpSeats[row, col] = 'L';
                                }
                                break;
                            case '.':
                                tmpSeats[row, col] = '.';
                                break;
                            default:
                                break;
                        }
                    }
                }

                seats1 = tmpSeats;     
            }

            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    if (seats1[row, col] == '#')
                        numberOfOccupiedSeats1++;
                }
            }


            // Part 2
            changed = true;
            while (changed)
            {
                changed = false;
                char[,] tmpSeats = new char[ROWS, COLUMNS];

                for (int row = 0; row < ROWS; row++)
                {
                    for (int col = 0; col < COLUMNS; col++)
                    {
                        int occupied = 0;

                        switch (seats2[row, col])
                        {
                            case '#':
                                // Right
                                for (int r = col + 1; r < COLUMNS; r++)
                                {
                                    if (seats2[row, r] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[row, r] == 'L')
                                        break;
                                }

                                // Right - Down
                                int ry = 1;
                                for (int rd = col + 1; rd < COLUMNS; rd++)
                                {
                                    if ((row + ry) == ROWS)
                                        break;

                                    if (seats2[row + ry, rd] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[row + ry, rd] == 'L')
                                        break;

                                    ry++;
                                }

                                // Down
                                for (int d = row + 1; d < ROWS; d++)
                                {
                                    if (seats2[d, col] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[d, col] == 'L')
                                        break;
                                }

                                // Left - Down
                                int ly = 1;
                                for (int ld = col - 1; ld >= 0; ld--)
                                {
                                    if ((row + ly) == ROWS)
                                        break;

                                    if (seats2[row + ly, ld] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[row + ly, ld] == 'L')
                                        break;

                                    ly++;
                                }

                                // Left 
                                for (int l = col - 1; l >= 0; l--)
                                {
                                    if (seats2[row, l] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[row, l] == 'L')
                                        break;
                                }

                                // Left - Up
                                ly = 1;
                                for (int lu = col - 1; lu >= 0; lu--)
                                {
                                    if ((row - ly) < 0)
                                        break;

                                    if (seats2[row - ly, lu] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[row - ly, lu] == 'L')
                                        break;

                                    ly++;
                                }

                                // Up
                                for (int u = row - 1; u >= 0; u--)
                                {
                                    if (seats2[u, col] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[u, col] == 'L')
                                        break;
                                }

                                // Right - Up
                                ry = 1;
                                for (int ru = col + 1; ru < COLUMNS; ru++)
                                {
                                    if ((row - ry) < 0)
                                        break;

                                    if (seats2[row - ry, ru] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[row - ry, ru] == 'L')
                                        break;

                                    ry++;
                                }

                                if (occupied >= 5)
                                {
                                    tmpSeats[row, col] = 'L';
                                    changed = true;
                                }
                                else
                                {
                                    tmpSeats[row, col] = '#';
                                }
                                break;
                            case 'L':
                                occupied = 0;

                                // Right
                                for (int r = col + 1; r < COLUMNS; r++)
                                {
                                    if (seats2[row, r] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[row, r] == 'L')
                                        break;
                                }

                                // Right - Down
                                int ry2 = 1;
                                for (int rd = col + 1; rd < COLUMNS; rd++)
                                {
                                    if ((row + ry2) == ROWS)
                                        break;

                                    if (seats2[row + ry2, rd] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[row + ry2, rd] == 'L')
                                        break;

                                    ry2++;
                                }

                                // Down
                                for (int d = row + 1; d < ROWS; d++)
                                {
                                    if (seats2[d, col] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[d, col] == 'L')
                                        break;
                                }

                                // Left - Down
                                ly = 1;
                                for (int ld = col - 1; ld >= 0; ld--)
                                {
                                    if ((row + ly) == ROWS)
                                        break;

                                    if (seats2[row + ly, ld] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[row + ly, ld] == 'L')
                                        break;

                                    ly++;
                                }

                                // Left 
                                for (int l = col - 1; l >= 0; l--)
                                {
                                    if (seats2[row, l] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[row, l] == 'L')
                                        break;
                                }

                                // Left - Up
                                ly = 1;
                                for (int lu = col - 1; lu >= 0; lu--)
                                {
                                    if ((row - ly) < 0)
                                        break;

                                    if (seats2[row - ly, lu] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[row - ly, lu] == 'L')
                                        break;

                                    ly++;
                                }

                                // Up
                                for (int u = row - 1; u >= 0; u--)
                                {
                                    if (seats2[u, col] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[u, col] == 'L')
                                        break;
                                }


                                // Right - Up
                                ry = 1;
                                for (int ru = col + 1; ru < COLUMNS; ru++)
                                {
                                    if ((row - ry) < 0)
                                        break;

                                    if (seats2[row - ry, ru] == '#')
                                    {
                                        occupied++;
                                        break;
                                    }
                                    if (seats2[row - ry, ru] == 'L')
                                        break;

                                    ry++;
                                }

                                if (occupied == 0)
                                {
                                    tmpSeats[row, col] = '#';
                                    changed = true;
                                }
                                else
                                {
                                    tmpSeats[row, col] = 'L';
                                }
                                break;
                            case '.':
                                tmpSeats[row, col] = '.';
                                break;
                            default:
                                break;
                        }
                    }
                }

                seats2 = tmpSeats;
            }

            numberOfOccupiedSeats2 = 0;
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    if (seats2[row, col] == '#')
                        numberOfOccupiedSeats2++;
                }
            }

            Console.WriteLine("Day10 (1): " + numberOfOccupiedSeats1);
            Console.WriteLine("      (2): " + numberOfOccupiedSeats2);
        }
    }
}