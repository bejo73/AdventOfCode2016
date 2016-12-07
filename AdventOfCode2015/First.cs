using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class First
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\First.txt");

            List<string> strings = new List<string>();

            int numberOfUps = 0;
            int numberOfDowns = 0;
            int level = 0;
            int position = 1;
            while ((line = file.ReadLine()) != null)
            {
                numberOfUps = numberOfUps + line.Count(f => f == '(');
                numberOfDowns = numberOfDowns + line.Count(f => f == ')');

                foreach(char c in line)
                {
                    if (c == '(')
                    {
                        level++;
                    }
                    else if (c == ')')
                    {
                        level--;
                    }

                    if (level < 0)
                    {
                        Console.WriteLine(position);
                        break;
                    }
                    position++;
                }
            }
            Console.WriteLine(numberOfUps - numberOfDowns);
       }
    }
}