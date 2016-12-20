using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Nineteen
    {
        public static void Run()
        {
            int NumberOfElves = 3018458; //5;

            List<Elf> elves = new List<Elf>();
            for (int i = 1; i <= NumberOfElves; i++)
            {
                elves.Add(new Elf { Number = i, Present = true });
            }

            int elvesLeft = NumberOfElves;

            List<Elf> currentElves = elves;
            List<Elf> nextElves = new List<Elf>();
            
            while (true)
            {
                nextElves = new List<Elf>();
                for ( int j = 0; j < currentElves.Count; j++)
                {
                    Elf current = currentElves[j];
                    
                    if (j == currentElves.Count -1 && current.Present == true)
                    {
                        nextElves.Add(current);
                        nextElves.RemoveAt(0);
                    }
                    else 
                    {
                        if (current.Present != false)
                        {
                            nextElves.Add(current);
                            currentElves[j+1].Present = false;
                        }
                    }            
                }
		
                currentElves = new List<Elf>();
                currentElves.AddRange(nextElves);
                
                if (nextElves.Count == 1)
                {
                    Elf last = nextElves.Single(l => l.Present == true);
                    Console.WriteLine(last.Number);
                    break;
                }
            }

        }

        public static void RunPart2()
        {
            int NumberOfElves = 5; // 3018458;

            List<Elf> elves = new List<Elf>();
            for (int i = 1; i <= NumberOfElves; i++)
            {
                elves.Add(new Elf { Number = i });
            }

            int indexOfCurrentElf = 0;

            while (elves.Count > 1)
            {
                int elfIndexToRemove = (elves.Count / 2) + indexOfCurrentElf;

                if (elfIndexToRemove >= elves.Count)
                {
                    elfIndexToRemove = elfIndexToRemove - elves.Count;
                    indexOfCurrentElf--;
                }

                elves.RemoveAt(elfIndexToRemove);

                indexOfCurrentElf++;

                if (indexOfCurrentElf >= elves.Count)
                {
                    indexOfCurrentElf = indexOfCurrentElf - elves.Count;
                }

            }

            Console.WriteLine(elves.First().Number);
        }

    }
}