using System;

namespace AdventOfCode.Helpers
{
    class Letter
    {
        public int Count { get; set; }
        public string Name { get; set; }

        public bool IsGreater(Letter letter)
        {
            bool result = false;

            if (this.Count > letter.Count)
            {
                result = true;
            }
            else if (this.Count == letter.Count)
            {
                if (Char.Parse(this.Name) < Char.Parse(letter.Name))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
