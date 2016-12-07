using System;
using System.IO;
using System.Collections.Generic;
using AdventOfCode.Helpers;

namespace AdventOfCode
{
    class Fourth
    {
        public static void Run()
        {
            //string inputa = "aaaaa-bbb-z-y-x-123[abxyz]";
            //string inputb = "a-b-c-d-e-f-g-h-987[abcde]";
            //string input = "not-a-real-room-404[oarel]";
            //string inputd = "totally-real-room-200[decoy]";

            //string inputB = "qzmt-zixmtkozy-ivhz-343";

            int sectorIdSum = 0;

            string line;
            StreamReader file = new StreamReader(@".\Data\rooms.txt");

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