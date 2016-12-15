using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Seventh
    {
        public static void Run()
        {
            //string address = "sabbag[afta]jkdjf";

            string line;
            StreamReader file = new StreamReader(@".\Data\Seventh.txt");

            List<string> strings = new List<string>();

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

            Console.WriteLine(numberOfAddressesWithTLSSupport);
            Console.WriteLine(numberOfAddressesWithSSLSupport);
        }
    }
}