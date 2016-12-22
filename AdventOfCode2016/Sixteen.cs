using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Sixteen
    {
        public static void Run()
        {
            int length = 35651584;//272
            string a = "11100010111110100";
            
            String output = String.Empty;
            while (true)
            {
                StringBuilder o = new StringBuilder();
                string b = a;

                b = new string(b.Reverse().ToArray());

                var result = b.Select(x => x == '1' ? '0' : (x == '0' ? '1' : x)).ToArray();
                b = new String(result);

                o.Append(a).Append("0").Append(b);

                if (o.Length >= length)
                {
                    Console.WriteLine("1");
                    output = o.ToString().Substring(0, length);
                    break;
                }
                a = o.ToString();
            }

            while (true)
            {
                StringBuilder u = new StringBuilder(); ;
                for (int i = 0; i < output.Length; i = i + 2)
                {
                    string j = output.Substring(i, 2);
                    if (Regex.IsMatch(j, @"(.)\1"))
                    {
                        u.Append("1");
                    }
                    else
                    {
                        u.Append("0");
                    }
                }

                if ((u.Length % 2) == 1)
                {
                    Console.WriteLine(u.Length);
                    Console.WriteLine(u);
                    break;
                }
                output = u.ToString();
            }
        }
    }
}