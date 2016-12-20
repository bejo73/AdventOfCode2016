using System;
using System.Linq;

namespace AdventOfCode
{
    class Eighteen
    {
        public static void Run()
        {
            string input = ".^^^^^.^^.^^^.^...^..^^.^.^..^^^^^^^^^^..^...^^.^..^^^^..^^^^...^.^.^^^^^^^^....^..^^^^^^.^^^.^^^.^^";

            int counter = input.Count(f => f == '.');
            Console.WriteLine(input);

            for (int i = 0; i < 400000 - 1; i++)
            {
                string newRow = "";
                for (int j = 0; j < input.Length; j++)
                {
                    if (j == 0)
                    {
                        newRow = newRow + GetTile("." + input.Substring(j, 2));
                    }
                    else if (j == (input.Length - 1))
                    {
                        newRow = newRow + GetTile(input.Substring(j - 1, 2) + ".");
                    }
                    else
                    {
                        newRow = newRow + GetTile(input.Substring(j - 1, 3));
                    }
                }

                input = newRow;
                Console.WriteLine(newRow);
                counter = counter + input.Count(f => f == '.');
            }

            Console.WriteLine(counter);
        }

        private static string GetTile(string v)
        {
            string result = ".";

            string a = v.Substring(0, 1);
            string c = v.Substring(2);

            if (a.Equals("^") || c.Equals("^"))
            {
                if (!(a.Equals("^") && c.Equals("^")))
                {
                    result = "^";
                }
            }

            return result;
        }
    }
}