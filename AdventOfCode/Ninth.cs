using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Ninth
    {

        public static void Run()
        {
            string str;

            List<string> strings = new List<string>();

            strings.Add("ADVENT");
            strings.Add("A(1x5)BC"); // ABBBBBC
            strings.Add("(3x3)XYZ"); // XYZXYZXYZ
            strings.Add("A(2x2)BCD(2x2)EFG"); // ABCBCDEFEFG
            strings.Add("(6x1)(1x3)A"); // (1x3)A
            strings.Add("X(8x2)(3x3)ABCY"); // X(3x3)ABC(3x3)ABCY

            foreach (string st in strings)
            {
                string s = st;
                StringBuilder sbs = new StringBuilder();

                while (true)
                {
                    s = decompress(s);
                    Console.WriteLine(s);
                    if (!s.Contains("("))
                    {
                        break;
                    }
                }
                Console.WriteLine(s.Length);
            }

            

            StringBuilder builder = new StringBuilder();
            Stream dest = File.OpenWrite(@".\Data\IdT2.txt");

            ;
            //char[] buffer = new char[4096];
            StreamWriter files = new System.IO.StreamWriter(@".\IT1.txt");

            using (StreamReader source = new StreamReader(@".\Data\IT1.txt"))
            {
                char[] buffer = new char[4096];
                int bytesRead;
                long counter = 0; ;
                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    //Console.WriteLine("bytesRead: " + bytesRead);

                    string s = new string(buffer);
                    //Console.WriteLine("string length: " + s.Length);



                    //Console.WriteLine(s.Substring(0, bytesRead));

                    string j = decompressChunk(s.Substring(0, bytesRead));
                    //string j = decompressChunk(s);

                    //Console.WriteLine(j.Length);
                    counter = counter + j.Length;
                    //dest.Write(Encoding.UTF8.GetBytes(j), 0, j.Length);
                    //builder.Append(j.Trim());

                    if (j.Contains("(")) Console.WriteLine("Markers exist");

                    files.Write(j); // "sb" is the StringBuilder

                }
                files.Flush();
                dest.Flush();
                Console.WriteLine("Counter: " + counter);
            }

            


            files.Close();


            //Console.WriteLine("Builder: " + builder.Length);
            //Console.WriteLine("Builder: " + builder.ToString());

            return;

            StreamReader file = new StreamReader(@".\Data\Ninth.txt");
            //char[] buffer = new char[4096];
            //file.Read(buffer, index, 1024);
            //file.

            while ((str = file.ReadLine()) != null)
            {
                string s = str;
                StringBuilder sbs = new StringBuilder();

                while (true)
                {
                    s = decompress(s);

                    System.IO.StreamWriter filed = new System.IO.StreamWriter(@".\2.txt");
                    filed.WriteLine(s); // "sb" is the StringBuilder

                    break;
                    Console.WriteLine(s);
                    if (!s.Contains("("))
                    {

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Onemore");
                        Console.WriteLine(s.Length);
                    }

                }

                Console.WriteLine(s.Length);
            }




        }

        private static string rest = String.Empty;


        private static string decompressChunk(string str)
        {
            string input = rest + str;

            StringBuilder output = new StringBuilder();
            while (input.Length > 0)
            {
                int markerStart = input.IndexOf("(");

                if (markerStart != -1)
                {
                    output.Append(input.Substring(0, markerStart));

                    int markerEnd = input.IndexOf(")", markerStart);
                    if (markerEnd == -1)
                    {
                        rest = input.Substring(markerStart);
                        //rest = input;
                        break;
                    }

                    string marker = input.Substring(markerStart + 1, markerEnd - (markerStart + 1));

                    input = input.Substring(markerEnd + 1);
                    
                    //Console.WriteLine(marker);
                    string[] split = marker.Split('x');
                    int numberOfLetters = Int32.Parse(split[0]);
                    int numberOfRepeats = Int32.Parse(split[1]);

                    if (numberOfLetters > input.Length)
                    {
                        //Console.WriteLine("numberOfLetters: " + numberOfLetters + "input.length: " + input.Length);
                        rest = "(" + marker + ")" + input;
                        //rest = input.Substring(markerStart);
                        break;
                    }

                    //input = input.Substring(markerEnd + 1);

                    string letters = input.Substring(0, numberOfLetters);

                    for (int i = 0; i < numberOfRepeats; i++)
                    {
                        output.Append(letters.Trim());
                    }
                    input = input.Substring(numberOfLetters);
                    rest = String.Empty;

                }
                else
                {
                    output.Append(input.Trim());
                    rest = String.Empty;
                    input = String.Empty;
                }
            }

            return output.ToString();
        }


        private static string decompress(string str)
        {
            string input = str;

            StringBuilder output = new StringBuilder();
            while (input.Length > 0)
            {
                int markerStart = input.IndexOf("(");

                if (markerStart != -1)
                {
                    output.Append(input.Substring(0, markerStart));

                    int markerEnd = input.IndexOf(")", markerStart);
                    if (markerEnd == -1)
                    {

                    }

                    string marker = input.Substring(markerStart + 1, markerEnd - (markerStart + 1));
                    //Console.WriteLine(marker);
                    string[] split = marker.Split('x');
                    int numberOfLetters = Int32.Parse(split[0]);
                    int numberOfRepeats = Int32.Parse(split[1]);

                    input = input.Substring(markerEnd + 1);

                    string letters = input.Substring(0, numberOfLetters);

                    for (int i = 0; i < numberOfRepeats; i++)
                    {
                        output.Append(letters);
                    }
                    input = input.Substring(numberOfLetters);

                }
                else
                {
                    output.Append(input);
                    input = String.Empty;
                }
            }

            return output.ToString();
        }

    }
}
