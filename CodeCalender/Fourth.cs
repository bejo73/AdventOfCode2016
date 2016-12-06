using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Letter
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

    public class Room
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

    class Fourth
    {
        public static void Run()
        {
            //string inputa = "aaaaa-bbb-z-y-x-123[abxyz]";
            //string inputb = "a-b-c-d-e-f-g-h-987[abcde]";
            //string input = "not-a-real-room-404[oarel]";
            //string inputd = "totally-real-room-200[decoy]";

            string inputB = "qzmt-zixmtkozy-ivhz-343";

            int sectorIdSum = 0;

            string line;
            StreamReader file = new StreamReader(@".\rooms.txt");

            List<Room> rooms = new List<Room>();

            while ((line = file.ReadLine()) != null)
            {
                Room room = new Room(line);
                
                if (room.IsValid())
                {
                    sectorIdSum = sectorIdSum + room.GetSectorId();
                    rooms.Add(room);
                }
            }

            Console.WriteLine("Sum of sector id: " + sectorIdSum);

            int alphabetLength = 26;

            foreach (Room room in rooms)
            {
                int shift = room.GetSectorId();
                if (shift > alphabetLength)
                {
                    shift = room.GetSectorId() % alphabetLength;
                }
                string decrypted = Decrypt(room.name, shift);
                if (decrypted.Contains("north"))
                {
                    Console.WriteLine(decrypted + ", SectorId: " + room.GetSectorId());
                }
            }
        }

        public static string Decrypt(string input, int shift)
        {
            char[] letters = input.ToCharArray();

            for (int i = 0; i < letters.Length; i++)
            {
                char letter = letters[i];

                if (letter == '-')
                {
                    letter = ' ';
                }
                else
                {
                    letter = (char)(letter + shift);
                    if (letter > 'z')
                    {
                        letter = (char)(letter - 26);
                    }
                    else if (letter < 'a')
                    {
                        letter = (char)(letter + 26);
                    }
                }

                letters[i] = letter;
            }

            return new string(letters);
        }
    }
}