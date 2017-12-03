using System;

namespace AdventOfCode2017
{
    class Day3
    {
        enum Direction { North, South, West, East };

        public static void Run()
        {
            int squareNumber = 312051;
            //Console.WriteLine(Math.Sqrt(squareNumber));

            double middle = Math.Round(Math.Sqrt(squareNumber)/2, 0);
            int size = (int)middle * 2 + 1;   
                     
            int[,] memoryGrid = new int[size, size];
            int[,] adjSquareSumGrid = new int[size, size];

            int zero = (int)middle;

            int num = 1;

            int currentX = zero;
            int currentY = zero;

            memoryGrid[currentX, currentY] = num;
            adjSquareSumGrid[currentX, currentY] = num;

            Direction direction = Direction.East;

            bool adjSquareSumFound = false;
            int adjSquareSum = 0;

            while (num < squareNumber)
            {
                num++;
                
                switch (direction)
                {
                    case Direction.East:
                        currentX++;
                        memoryGrid[currentX, currentY] = num;
                        if (memoryGrid[currentX, currentY + 1] == 0)
                        {
                            direction = Direction.North;
                        }
                        break;
                    case Direction.North:
                        currentY++;
                        memoryGrid[currentX, currentY] = num;
                        if (memoryGrid[currentX - 1, currentY] == 0)
                        {
                            direction = Direction.West;
                        }
                        break;
                    case Direction.West:
                        currentX--;
                        memoryGrid[currentX, currentY] = num;
                        if (memoryGrid[currentX, currentY - 1] == 0)
                        {
                            direction = Direction.South;
                        }
                        break;
                    case Direction.South:
                        currentY--;
                        memoryGrid[currentX, currentY] = num;
                        if (memoryGrid[currentX + 1, currentY] == 0)
                        {
                            direction = Direction.East;
                        }
                        break;
                }

                if (!adjSquareSumFound)
                {
                    adjSquareSumGrid[currentX, currentY] = getSum(adjSquareSumGrid, currentX, currentY);
                    if (adjSquareSumGrid[currentX, currentY] > squareNumber)
                    {
                        adjSquareSum = adjSquareSumGrid[currentX, currentY];
                        adjSquareSumFound = true;
                    }
                }

            }

            Console.WriteLine("Day 3 (1): " + (Math.Abs((currentX - zero)) + Math.Abs((currentY - zero))));
            Console.WriteLine("      (2): " + adjSquareSum);
        }

        private static int getSum(int[,] memoryGrid, int x, int y)
        {
            int sum = 0;

            for (int i = x - 1; i <= x + 1 ; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    sum = sum + memoryGrid[i, j];
                }
            }

            sum = sum - memoryGrid[x, y];

            return sum;
        }

    }
}