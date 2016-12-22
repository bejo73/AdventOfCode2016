namespace AdventOfCode.Helpers
{
    class Disc
    {
        private int startPosition;
        private int numberOfPositions;

        public Disc(int startPosition, int numberOfPositions)
        {
            this.startPosition = startPosition;
            this.numberOfPositions = numberOfPositions;
        }

        public bool IsZero(int time)
        {
            if (startPosition == 0)
            {
                return (time % numberOfPositions) == 0;
            }

            return ((time % numberOfPositions) - (numberOfPositions - startPosition) == 0);
        }
    }
}