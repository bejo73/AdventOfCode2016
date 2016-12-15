using System.Collections.Generic;

namespace AdventOfCode.Helpers
{
    class Floor
    {
        public int Number { get; set; }

        public int Count()
        {
            return Generators.Count + Microchips.Count;
        }

        public List<Generator> Generators = new List<Generator>();
        public List<Microchip> Microchips = new List<Microchip>();
    }
}
