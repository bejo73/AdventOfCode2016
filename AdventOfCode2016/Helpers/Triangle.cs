namespace AdventOfCode.Helpers
{
    class Triangle
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }

        public Triangle(int a, int b, int c)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }

        public bool IsValid()
        {
            return (A + B > C) && (A + C > B) && (B + C > A);
        }
    }
}