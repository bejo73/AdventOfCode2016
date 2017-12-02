using System;
using System.IO;

namespace AdventOfCode2017
{
    class Day2
    {
        public static void Run()
        {

            string line;
            StreamReader file = new StreamReader(@".\Data\2.txt");
            char separator = '\t';

            int sum = 0;
            int sum2 = 0;

            while ((line = file.ReadLine()) != null)
            {
                int diff = getDiff(line, separator);
                sum = sum + diff;

                int divide = getDivide(line, separator);
                sum2 = sum2 + divide;
            }

            Console.WriteLine("Day2 (1): " + sum);
            Console.WriteLine("     (2): " + sum2);
        }

        private static int getDivide(string line, char separator)
        {
            int result = 0;
            string[] numbers = line.Split(separator);

            for (int i = 0; i < numbers.Length; i++)
            {
                int first = int.Parse(numbers[i]);

                for (int j = i + 1; j < numbers.Length; j++)
                {
                    int second = int.Parse(numbers[j]);

                    if ((first % second) == 0)
                    {
                        return first / second;
                    }
                    else
                    {
                        if ((second % first) == 0)
                        {
                            return second / first;
                        }
                    }
                }
            }

            return result;
        }

        private static int getDiff(string line, char separator)
        {
            int diff = 0;

            int max = int.MinValue;
            int min = int.MaxValue;

            string[] numbers = line.Split(separator);

            foreach (string n in numbers)
            {
                int val = int.Parse(n);

                if (val > max) max = val;
                if (val < min) min = val;
            }

            diff = max - min;

            return diff;
        }
    }
}