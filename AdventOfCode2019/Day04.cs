using System;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day04
    {
        // 1 => 1650
        // 2 => 1129

        public static void Run()
        {
            int start = 178416;
            int end   = 676461;

            int validCounter = 0;

            for (int i = start; i <= end; i++)
            {
                string password = i.ToString();

                bool two = false;
                bool increase = true;

                bool even = false;

                for (int j = 0; j < 5; j++)
                {
                    char a = password[j];
                    char b = password[j + 1];

                    if (a.Equals(b))
                    {
                        if (even)
                        {
                            even = false;
                        }
                        else
                        {
                            even = true;
                            two = true;
                        }
                    }
                    
                    if (a > b)
                    {
                        increase = false;
                        break;
                    }
                }

                if (two && increase)
                {
                    validCounter++;

                }
            }
            
            Console.WriteLine("Day4 (1): " + validCounter);
            
            int[] digits;

            bool adjacent;
            bool falling;
            int count = 0;

            for (int i = start; i <= end; i++)
            {
                digits = i.ToString().ToCharArray().Select(f => (int)Char.GetNumericValue(f)).ToArray();

                adjacent = false;
                falling = true;

                for (int j = 0; j < 5; j++)
                {
                    if (adjacent == false) // Single 2-group already found
                    {
                        if (digits[j] == digits[j + 1])
                        {
                            adjacent = true;
                        }

                        if (j < 4 && digits[j] == digits[j + 2])
                        {
                            adjacent = false;
                        }

                        if (j > 0 && digits[j] == digits[j - 1])
                        {
                            adjacent = false;
                        }
                    }

                    if (digits[j + 1] < digits[j])
                    {
                        falling = false;
                    }
                }

                if (adjacent == true && falling == true)
                {
                    count++;
                }
            }

            Console.WriteLine("     (2): " + count);
        }
    }
}