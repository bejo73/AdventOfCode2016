using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /* 
     * Test
     * 
     * The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.
     * The second floor contains a hydrogen generator.
     * The third floor contains a lithium generator.
     * The fourth floor contains nothing relevant.
     */

    /*
     * A
     * 
     * The first floor contains a thulium generator, a thulium-compatible microchip, a plutonium generator, and a strontium generator.
     * The second floor contains a plutonium-compatible microchip and a strontium-compatible microchip.
     * The third floor contains a promethium generator, a promethium-compatible microchip, a ruthenium generator, and a ruthenium-compatible microchip.
     * The fourth floor contains nothing relevant.
     */

    /*
     * B
     * 
     * All in A and theese on first floor
     * An elerium generator. An elerium-compatible microchip.
     * A dilithium generator. A dilithium-compatible microchip.
     */

    class Eleven
    {
        public static void Run()
        {
            Floor[] floors = new Floor[4];
            floors[0] = new Floor() { Number = 1 };
            floors[1] = new Floor() { Number = 2 };
            floors[2] = new Floor() { Number = 3 };
            floors[3] = new Floor() { Number = 4 };

            Elevator e = new Elevator();

            /*
            // TEST

            // Team Hydrogen
            int teamId = 1;
            Generator g = new Generator() { TeamId = teamId, Floor = 2 };
            Microchip m = new Microchip() { TeamId = teamId, Floor = 1 };

            floors[0].microchips.Add(m);
            floors[1].generators.Add(g);

            // Team Lithium
            teamId = 2;
            g = new Generator() { TeamId = teamId, Floor = 3 };
            m = new Microchip() { TeamId = teamId, Floor = 1 };

            floors[0].microchips.Add(m);
            floors[2].generators.Add(g);
            */

            // A

            // Thulium
            int teamId = 1;
            Generator g = new Generator() { TeamId = teamId, Tied = true };
            Microchip m = new Microchip() { TeamId = teamId, Tied = true };

            floors[0].Microchips.Add(m);
            floors[0].Generators.Add(g);

            // Plutonium
            teamId = 2;
            g = new Generator() { TeamId = teamId };
            m = new Microchip() { TeamId = teamId };

            floors[1].Microchips.Add(m);
            floors[0].Generators.Add(g);

            // Strontium
            teamId = 3;
            g = new Generator() { TeamId = teamId };
            m = new Microchip() { TeamId = teamId };

            floors[1].Microchips.Add(m);
            floors[0].Generators.Add(g);

            // Promethium
            teamId = 4;
            g = new Generator() { TeamId = teamId, Tied = true };
            m = new Microchip() { TeamId = teamId, Tied = true };

            floors[2].Microchips.Add(m);
            floors[2].Generators.Add(g);

            // Ruthenium
            teamId = 5;
            g = new Generator() { TeamId = teamId, Tied = true };
            m = new Microchip() { TeamId = teamId, Tied = true };

            floors[2].Microchips.Add(m);
            floors[2].Generators.Add(g);

            // B
            
            // Elerium
            teamId = 6;
            g = new Generator() { TeamId = teamId, Tied = true };
            m = new Microchip() { TeamId = teamId, Tied = true };

            floors[0].Microchips.Add(m);
            floors[0].Generators.Add(g);

            // Dilithium
            teamId = 7;
            g = new Generator() { TeamId = teamId, Tied = true };
            m = new Microchip() { TeamId = teamId, Tied = true };

            floors[0].Microchips.Add(m);
            floors[0].Generators.Add(g);

            int currentLevel = 0;
            int elevatorRides = 0;

            for (int loadingLevel = 0; loadingLevel < 3; loadingLevel++)
            {
                currentLevel = loadingLevel;
                while (floors[loadingLevel].Count() > 0)
                {
                    int nextLevel = loadingLevel + 1;

                    if (currentLevel == loadingLevel)
                    {
                        e = LoadElevator(floors[loadingLevel], floors[nextLevel]);

                        floors[nextLevel].Generators.AddRange(e.Generators);
                        floors[nextLevel].Microchips.AddRange(e.Microchips);

                        foreach (Generator fg in floors[nextLevel].Generators)
                        {
                            fg.Tied = floors[nextLevel].Microchips.Any(mf => mf.TeamId == fg.TeamId);
                        }
                        foreach (Microchip mg in floors[nextLevel].Microchips)
                        {
                            mg.Tied = floors[nextLevel].Generators.Any(gg => gg.TeamId == mg.TeamId);
                        }

                        currentLevel = nextLevel;
                    }
                    else if (currentLevel == nextLevel)
                    {
                        e = LoadElevatorDown(floors[nextLevel], floors[loadingLevel]);

                        floors[loadingLevel].Generators.AddRange(e.Generators);
                        floors[loadingLevel].Microchips.AddRange(e.Microchips);

                        foreach (Generator fg in floors[loadingLevel].Generators)
                        {
                            fg.Tied = floors[loadingLevel].Microchips.Any(mf => mf.TeamId == fg.TeamId);
                        }
                        foreach (Microchip mg in floors[loadingLevel].Microchips)
                        {
                            mg.Tied = floors[loadingLevel].Generators.Any(gg => gg.TeamId == mg.TeamId);
                        }

                        currentLevel = loadingLevel;
                    }

                    elevatorRides++;
                }
            }
           
            Console.WriteLine("" + elevatorRides);
        }

        private static Elevator LoadElevator(Floor current, Floor next)
        {
            Elevator e = new Elevator();

            if (current.Generators.Count > 0)
            {
                List<Generator> unTied = current.Generators.Where(g => g.Tied == false).ToList();

                foreach (Generator ug in unTied)
                {
                    Microchip um = next.Microchips.Single(m => m.TeamId == ug.TeamId);
                    if (um != null)
                    {
                        e.Generators.Add(ug);
                    }

                    if (e.Generators.Count == 2)
                    {
                        break;
                    }
                }

                if (e.Generators.Count == 2)
                {

                }
                else if (e.Generators.Count == 1)
                {
                    if (next.Microchips.Count(km => km.Tied == false) < 2)
                    {
                        Generator gf = current.Generators.First(ds => ds.Tied == true);
                        e.Generators.Add(gf);
                    }
                }
                else
                {
                    Generator gf = current.Generators.First(ds => ds.Tied == true);
                    e.Generators.Add(gf);

                    Microchip fg = current.Microchips.First(df => df.TeamId == gf.TeamId);
                    e.Microchips.Add(fg);
                }
            }
            else
            {
                int c = 0;
                foreach (Microchip m in current.Microchips)
                {
                    if (next.Generators.Any(g => g.TeamId == m.TeamId))
                    {
                        e.Microchips.Add(m);
                        c++;
                    }
                    else
                    {
                        e.Microchips.Add(m);
                        c++;
                    }

                    if (c == 2)
                    {
                        break;
                    }
                }
            }

            foreach (Generator sfd in e.Generators)
            {
                current.Generators.Remove(sfd);
            }
            foreach (Microchip mfs in e.Microchips)
            {
                current.Microchips.Remove(mfs);
            }

            return e;
        }


        private static Elevator LoadElevatorDown(Floor current, Floor next)
        {
            Elevator e = new Elevator();

            if (next.Generators.Count > 0)
            {
                Generator gf = null;
                if (current.Generators.Any(ds => ds.Tied == true))
                {
                    gf = current.Generators.First(ds => ds.Tied == true);
                }
                else
                {
                    gf = current.Generators.First(ds => ds.Tied == false);
                }

                if (gf != null)
                {
                    
                    e.Generators.Add(gf);
                }
                else
                {
                    Microchip mf = current.Microchips.First(df => df.Tied == false);
                    e.Microchips.Add(mf);
                }
            }
            else if (next.Microchips.Count > 0)
            {
                Microchip mf = null;

                if (current.Microchips.Any(d1f => d1f.Tied == true))
                {
                    mf = current.Microchips.First(d2f => d2f.Tied == true);
                    mf.Tied = false;
                }
                else
                {
                    mf = current.Microchips.First(d3f => d3f.Tied == false);
                }

                if (mf != null)
                {
                    e.Microchips.Add(mf);
                }
                else
                {
                    Generator gf = current.Generators.First(ds => ds.Tied == false);
                    if (gf == null)
                    {
                        gf = current.Generators.First(ds => ds.Tied == true);
                        
                    }
                    e.Generators.Add(gf);
                }
            }

            foreach (Generator sfd in e.Generators)
            {
                current.Generators.Remove(sfd);
            }
            foreach (Microchip mfs in e.Microchips)
            {
                current.Microchips.Remove(mfs);
            }

            return e;
        }
    }
}