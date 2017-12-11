using System;
using System.IO;

namespace AdventOfCode2017
{
    class Day9
    {
        
        public static void Run()
        {

            StreamReader file = new StreamReader(@".\Data\9.txt");

            int currentLevel = 0;
            int score = 0;
            int garbage = 0;
            while(!file.EndOfStream)
            {
                char ch = (char)file.Read();
                //Console.WriteLine(ch);

                switch (ch)
                {
                    case '<':
                        while (!file.EndOfStream)
                        {                          
                            ch = (char)file.Read();
                            garbage++;
                            if (ch == '!')
                            {
                                garbage--;
                                file.Read();
                            }
                            else if (ch == '>')
                            {
                                garbage--;
                                break;
                            }
                        }
                        break;
                    case '{':
                        currentLevel++;
                        break;
                    case '}':
                        score = score + currentLevel;
                        currentLevel--;
                        break;
                }
            }

            Console.WriteLine("Day9 (1): " + score);
            Console.WriteLine("     (2): " + garbage);
        }
    }
}