namespace AdventOfCode.Helpers
{
    class Node
    {
        public Node ParentNode { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public char Id { get; set; }

        public int Cost { get; set; }
    }
}