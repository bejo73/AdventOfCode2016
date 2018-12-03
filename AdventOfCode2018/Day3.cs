using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode2018
{
    class Day3
    {
        public static void Run()
        {
            int size = 1000;            
            char[,] grid = new char[size, size];

            for (int i = 0; i < size;i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = '.';
                }
            }

            string line;
            StreamReader file = new StreamReader(@".\Data\3.txt");

            // #1 @ 1,3: 4x4
            string pattern = @"#([0-9]*) @ ([0-9]*),([0-9]*): ([0-9]*)x([0-9]*)";

            int counter = 0;

            while ((line = file.ReadLine()) != null)
            { 
                MatchCollection matches = Regex.Matches(line, pattern);

                foreach (Match match in matches)
                {
                    string id = match.Groups[1].Value;
                    int x = Int32.Parse(match.Groups[2].Value);
                    int y = Int32.Parse(match.Groups[3].Value);
                    int xLength = Int32.Parse(match.Groups[4].Value);
                    int yLength = Int32.Parse(match.Groups[5].Value);

                    for (int i = x; i < x + xLength; i++)
                    {
                        for (int j = y; j < y + yLength; j++)
                        {
                            if (grid[i, j] == '.')
                            {
                                grid[i, j] = '#';
                            }
                            else if (grid[i, j] == 'X')
                            {
                            }
                            else
                            {
                                grid[i, j] = 'X';
                                counter++;
                            }
                        }
                    }
                }
            }
            file.Close();

            file = new StreamReader(@".\Data\3.txt");
            string claimId = string.Empty;

            while ((line = file.ReadLine()) != null)
            {
                MatchCollection matches = Regex.Matches(line, pattern);

                foreach (Match match in matches)
                {
                    string id = match.Groups[1].Value;
                    int x = Int32.Parse(match.Groups[2].Value);
                    int y = Int32.Parse(match.Groups[3].Value);
                    int xLength = Int32.Parse(match.Groups[4].Value);
                    int yLength = Int32.Parse(match.Groups[5].Value);

                    bool inter = false;

                    for (int i = x; i < x + xLength; i++)
                    {
                        for (int j = y; j < y + yLength; j++)
                        {
                            if (grid[i, j] == 'X')
                            {
                                inter = true;
                            }
                            
                        }
                    }

                    if (!inter)
                    {
                        claimId = id;
                    }
                }
            }
            file.Close();

            /*
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }
            */

            Console.WriteLine("Day 3 (1): " + counter);
            Console.WriteLine("      (2): " + claimId);
        }
    }
}