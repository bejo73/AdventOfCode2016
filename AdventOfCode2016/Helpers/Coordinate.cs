namespace AdventOfCode.Helpers
{
    class Coordinate
    {
        public int x { get; set; }

        public int y { get; set; }

        public Coordinate() { }

        public string getUnique()
        {
            return "" + x + y;
        }
    }
}
