using System.Collections.Generic;

namespace AdventOfCode.Helpers
{
    class Elevator
    {
        public List<Generator> Generators;
        public List<Microchip> Microchips;

        public Elevator()
        {
            Generators = new List<Generator>();
            Microchips = new List<Microchip>();
        }
    }
}
