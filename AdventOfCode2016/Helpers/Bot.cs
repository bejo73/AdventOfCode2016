using System;

namespace AdventOfCode.Helpers
{
    public class Bot
    {
        private int id;
        private int microshipHigh;
        private int microshipLow;

        public int MicroshipHigh { get { return this.microshipHigh; } }

        public int MicroshipLow { get { return this.microshipLow; } }

        public int Id { get { return this.id; } }

        public int BotIdHigh { get; set; }

        public int BotIdLow { get; set; }

        public int OutputHigh { get; set; }
        public int OutputLow { get; set; }

        public Bot(int id)
        {
            this.id = id;
            this.microshipLow = int.MinValue;
            this.microshipHigh = int.MinValue;
            BotIdLow = int.MinValue;
            BotIdHigh = int.MinValue;
        }

        public bool ReceiveMicroship(int value)
        {
            bool result = false;
            if (MicroshipLow == int.MinValue)
            {
                this.microshipLow = value;
            }
            else
            {
                result = true;
                if (value > MicroshipLow)
                {
                    this.microshipHigh = value;
                }
                else
                {
                    this.microshipHigh = MicroshipLow;
                    this.microshipLow = value;
                }
            }

            if (this.microshipHigh == 61 && this.microshipLow == 17)
            {
                Console.WriteLine("BOT #{0} is comparing {1} and {2}", this.Id, this.microshipHigh, this.microshipLow);
            }

            return result; 
        }

        public void ResetMicroships()
        {
            this.microshipLow = int.MinValue;
            this.microshipHigh = int.MinValue;
        }

    }
}