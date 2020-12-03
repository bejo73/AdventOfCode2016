using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020
{
    class Day01
    {

        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@"./Data/Day01.txt");

            int result1 = 0;
            int result2 = 0;

            List<int> expenseList = new List<int>();

            while ((line = file.ReadLine()) != null)
            {
                expenseList.Add(int.Parse(line));
            }

            // 1
            for (int i = 0; i < expenseList.Count; i++)
            {
                for (int j = i + 1; j < expenseList.Count; j++)
                {
                    if (expenseList[i] + expenseList[j] == 2020)
                    {
                        result1 = expenseList[i] * expenseList[j];
                        break;
                    }
                }

                if (result1 > 0)
                    break;
            }

            // 2
            for (int i = 0; i < expenseList.Count; i++)
            {
                for (int j = i + 1; j < expenseList.Count; j++)
                {
                    if ((expenseList[i] + expenseList[j]) < 2020)
                    {
                        for (int k = j + 1; k < expenseList.Count; k++)
                        {
                            if (expenseList[i] + expenseList[j] + expenseList[k] == 2020)
                            {
                                result2 = expenseList[i] * expenseList[j] * expenseList[k];
                                break;
                            }       
                        }
                    }

                    if (result2 > 0)
                        break;
                }

                if (result2 > 0)
                    break;
            }

            /* Nice read of numbers found
            int[] numbers = File.ReadAllLines(@"./Data/Day01.txt").Select(Int32.Parse).ToArray();

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    for (int k = j + 1; k < numbers.Length; k++)
                    {
                        if (numbers[i] + numbers[j] + numbers[k] == 2020)
                        {
                            Console.WriteLine($"{numbers[i]} + {numbers[j]} + {numbers[k]} = 2020; {numbers[i]} * {numbers[j]} * {numbers[k]} = {numbers[i] * numbers[j] * numbers[k]}");
                        }
                    }
                }
            }
            */

            Console.WriteLine("Day1 (1): " + result1);
            Console.WriteLine("     (2): " + result2);
        }
    }
}
