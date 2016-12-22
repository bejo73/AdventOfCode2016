using AdventOfCode.Helpers;
using System;
using System.IO;

namespace AdventOfCode
{
    class Seventh
    {
        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\7.txt");

            int numberOfAddressesWithTLSSupport = 0;
            int numberOfAddressesWithSSLSupport = 0;

            while ((line = file.ReadLine()) != null)
            {
                IPv7 ip = new IPv7(line);

                if (ip.HasTLSSupport())
                {
                    numberOfAddressesWithTLSSupport++;
                }
                if (ip.HasSSLSupport())
                {
                    numberOfAddressesWithSSLSupport++;
                }
            }

            Console.WriteLine("TLS: " + numberOfAddressesWithTLSSupport);
            Console.WriteLine("SSL: " + numberOfAddressesWithSSLSupport);
        }
    }
}