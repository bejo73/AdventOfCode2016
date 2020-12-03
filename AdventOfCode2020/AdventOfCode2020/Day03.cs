using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
    class Day03
    {

        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day03.txt");

            int treeCounter = 0;

            long treeCounter1 = 0;
            long treeCounter2 = 0;
            long treeCounter3 = 0;
            long treeCounter4 = 0;
            long treeCounter5 = 0;

            List<string> grid = new List<string>();
            List<StringBuilder> largeGrid = new List<StringBuilder>();

            while ((line = file.ReadLine()) != null)
            {
                grid.Add(line.Trim());
            }

            int rows = grid.Count;

            for (int i = 0; i < rows; i++)
            {
                largeGrid.Add(new StringBuilder());
                for (int j = 0; j < rows; j++)
                {
                    largeGrid[i].Append(grid[i]);
                }
            }
            
            for (int k = 1; k < rows; k++)
            {
                string item = largeGrid[k].ToString().Substring(k * 3, 1);

                if (item.Equals("#"))
                {
                    treeCounter++;
                }

                item = largeGrid[k].ToString().Substring(k * 1, 1);

                if (item.Equals("#"))
                {
                    treeCounter1++;
                }

                item = largeGrid[k].ToString().Substring(k * 3, 1);

                if (item.Equals("#"))
                {
                    treeCounter2++;
                }

                item = largeGrid[k].ToString().Substring(k * 5, 1);

                if (item.Equals("#"))
                {
                    treeCounter3++;
                }

                item = largeGrid[k].ToString().Substring(k * 7, 1);

                if (item.Equals("#"))
                {
                    treeCounter4++;
                }

                if (k % 2 == 0)
                {
                    item = largeGrid[k].ToString().Substring(k / 2 * 1, 1);

                    if (item.Equals("#"))
                    {
                        treeCounter5++;
                    }
                }
                
            }

            long prod = treeCounter1 * treeCounter2 * treeCounter3 * treeCounter4 * treeCounter5;

            Console.WriteLine("Day3 (1): " + treeCounter);
            Console.WriteLine("     (2): " + prod);
        }
    }
}
