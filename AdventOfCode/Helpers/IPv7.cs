using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Helpers
{
    class IPv7
    {
        public string Address { get; set; }

        private bool tlsSupport { get; set; }

        private bool sslSupport { get; set; }

        private HashSet<string> aba = new HashSet<string>();
        private HashSet<string> bab = new HashSet<string>();

        public IPv7(string address)
        {
            this.Address = address;
            checkForTLS();
            checkForSSL();
        }

        private void checkForTLS()
        {
            Match match = Regex.Match(this.Address, @"(([a-z]*){1}([\[]([a-z]*)[\]]){0,1})*");

            if (match.Success)
            {
                // Supernet
                foreach (Capture capture in match.Groups[2].Captures)
                {
                    string sequence = capture.Value;

                    // Find ABBA
                    while (sequence.Length >= 4)
                    {
                        if (checkForABBA(sequence.Substring(0, 4)))
                        {
                            this.tlsSupport = true;
                            break;
                        }   
                        sequence = sequence.Substring(1);
                    }

                    // Find BAB
                    sequence = capture.Value;
                    while (sequence.Length >= 3)
                    {
                        if (checkForABA(sequence.Substring(0, 3)))
                        {
                            aba.Add(sequence.Substring(0, 3));
                        }
                        sequence = sequence.Substring(1);
                    }
                }

                // Hypernet
                foreach (Capture capture in match.Groups[4].Captures)
                {
                    string sequence = capture.Value;

                    // Find ABBA
                    while (sequence.Length >= 4)
                    {
                        if (checkForABBA(sequence.Substring(0, 4)))
                        {
                            this.tlsSupport = false;
                            break;
                        }
                        sequence = sequence.Substring(1);
                    }

                    // Find BAB
                    sequence = capture.Value;
                    while (sequence.Length >= 3)
                    {
                        if (checkForABA(sequence.Substring(0, 3)))
                        {
                            bab.Add(sequence.Substring(0, 3));
                        }
                        sequence = sequence.Substring(1);
                    }
                }
            }
            else
            {
                Console.WriteLine("Not a valid IPv7 address");
            }
        }

        private void checkForSSL()
        {
            foreach (string str in aba)
            {
                string tmp = str.Substring(1) + str.Substring(1, 1);
                if (bab.Contains(tmp))
                {
                    this.sslSupport = true;
                }
            }
        }

        private bool checkForABBA(string input)
        {
            bool result = false;
            string first = input.Substring(0, 2);
            string last  = input.Substring(2, 2);

            if (!first.Equals(last))
            {
                last = Reverse(last);
                if (first.Equals(last))
                {
                    result = true;
                }
            }
            return result;
        }

        private string Reverse(string str)
        {
            char[] array = str.ToCharArray();
            Array.Reverse(array);

            return new String(array);
        }

        private bool checkForABA(string input)
        {
            bool result = false;
            string first  = input.Substring(0, 1);
            string middle = input.Substring(1, 1);
            string last   = input.Substring(2, 1);

            if (first.Equals(last) && !first.Equals(middle))
            {
                result = true;
            }
            return result;
        }

        public bool HasTLSSupport()
        {
            return this.tlsSupport;
        }

        public bool HasSSLSupport()
        {
            return this.sslSupport;
        }
    }
}