using System;
using System.Linq;

namespace AdventOfCode.Helpers
{
    class Room
    {
        private string checksum;
        private int sectorId;
        public string name { get; }
        public string room { get; }
        private string createdChecksum;

        public int GetSectorId()
        {
            return sectorId;
        }

        public Room(string room)
        {
            this.room = room;

            int checksumStart = room.IndexOf("[");
            this.checksum = room.Substring(checksumStart + 1, 5);

            int sectorIdStart = room.LastIndexOf("-");
            this.sectorId = Int32.Parse(room.Substring(sectorIdStart + 1, (checksumStart - (sectorIdStart + 1))));

            this.name = room.Substring(0, sectorIdStart);
            this.CreateCheckSum();
        }

        public void CreateCheckSum()
        {
            string tmpName = this.name.Replace("-", "");
            string distinct = String.Concat(tmpName.OrderBy(c => c).Distinct());

            Letter[] letters = new Letter[5];

            while (distinct.Length > 0)
            {
                string letter = distinct.Substring(0, 1);
                distinct = distinct.Substring(1);

                int count = tmpName.Count(f => f == Char.Parse(letter));

                Letter newLetter = new Letter() { Count = count, Name = letter };

                for (int i = 0; i < 5; i++)
                {
                    if (letters[i] != null)
                    {
                        if (newLetter.IsGreater(letters[i]))
                        {
                            switch (i)
                            {
                                case 0:
                                    letters[i + 4] = letters[i + 3];
                                    goto case 1;
                                case 1:
                                    letters[i + 3] = letters[i + 2];
                                    goto case 2;
                                case 2:
                                    letters[i + 2] = letters[i + 1];
                                    goto case 3;
                                case 3:
                                    letters[i + 1] = letters[i];
                                    goto default;
                                default:
                                    letters[i] = newLetter;
                                    break;
                            }
                            break;
                        }
                    }
                    else
                    {
                        letters[i] = newLetter;
                        break;
                    }
                }
            }

            createdChecksum = letters[0].Name + letters[1].Name + letters[2].Name + letters[3].Name + letters[4].Name;
        }

        public bool IsValid()
        {
            return checksum.Equals(createdChecksum);
        }
    }
}