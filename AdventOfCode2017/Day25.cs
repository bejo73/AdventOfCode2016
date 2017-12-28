using System;
using System.Linq;

namespace AdventOfCode2017
{
    public class Day25
    {
        public static void Run()
        {
            int steps = 12425180;

            int[] tape = new int[steps + 1];

            int currentPosition = steps/2;

            string state = "A";

            for (int i = 0; i < steps; i++)
            {
                switch (state)
                {
                    case "A":
                        if (tape[currentPosition] == 0)
                        {
                            tape[currentPosition] = 1;
                            currentPosition++;
                            state = "B";
                        }
                        else
                        {
                            tape[currentPosition] = 0;
                            currentPosition++;
                            state = "F";
                        }
                        break;
                    case "B":
                        if (tape[currentPosition] == 0)
                        {
                            tape[currentPosition] = 0;
                            currentPosition--;
                        }
                        else
                        {
                            tape[currentPosition] = 1;
                            currentPosition--;
                            state = "C";
                        }
                        break;
                    case "C":
                        if (tape[currentPosition] == 0)
                        {
                            tape[currentPosition] = 1;
                            currentPosition--;
                            state = "D";
                        }
                        else
                        {
                            tape[currentPosition] = 0;
                            currentPosition++;
                        }
                        break;
                    case "D":
                        if (tape[currentPosition] == 0)
                        {
                            tape[currentPosition] = 1;
                            currentPosition--;
                            state = "E";
                        }
                        else
                        {
                            tape[currentPosition] = 1;
                            currentPosition++;
                            state = "A";
                        }
                        break;
                    case "E":
                        if (tape[currentPosition] == 0)
                        {
                            tape[currentPosition] = 1;
                            currentPosition--;
                            state = "F";
                        }
                        else
                        {
                            tape[currentPosition] = 0;
                            currentPosition--;
                            state = "D";
                        }
                        break;
                    case "F":
                        if (tape[currentPosition] == 0)
                        {
                            tape[currentPosition] = 1;
                            currentPosition++;
                            state = "A";
                        }
                        else
                        {
                            tape[currentPosition] = 0;
                            currentPosition--;
                            state = "E";
                        }
                        break;
                }
            }

            Console.WriteLine("Day25 (1): " + tape.Count(s => s == 1));
            Console.WriteLine("      (2): Must have 50 stars" );
        }
    }
}