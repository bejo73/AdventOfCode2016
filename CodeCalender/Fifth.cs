using System;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode
{
    class Fifth
    {
        public static void RunB()
        {
            int counter = 0;

            string[] p = new string[8];
            bool passwordIncomplete = true;

            while (passwordIncomplete)
            {
                string hash;
                while (true)
                {
                    string input = "cxdnnyjw" + counter;
                    hash = CreateMD5(input);

                    if (hash.StartsWith("00000"))
                    {
                        Console.WriteLine(input + ", " + hash);
                        counter++;
                        break;
                    }
                    counter++;
                }
                
                string pos = hash.Substring(5, 1);
                int position = Int32.MaxValue;
                try
                {
                    position = Int32.Parse(pos);
                }
                catch (Exception) { }

                //Int32.TryParse(pos, out position);
                
                    if (position < 8)
                    {
                        if (p[position] == null)
                        {
                            Console.WriteLine("Adding, pos=" + pos + ", val="+ hash.Substring(6, 1));
                            p[position] = hash.Substring(6, 1);
                        }
                    }
                

                passwordIncomplete = false;

                for (int j = 0; j < 8; j++)
                {
                    if (p[j] == null )
                    {
                        Console.WriteLine("Pos="+j+", is null");
                        passwordIncomplete = true;
                    }
                }
            }

            for (int j = 0; j < 8; j++)
            {
                Console.Write(p[j]);
            }
        }

        public static void RunA()
        {
            int counter = 0;

            string[] p = new string[8];

            for (int i = 0; i < 8; i++)
            {
                string hash;
                while (true)
                {
                    string input = "cxdnnyjw" + counter;
                    hash = CreateMD5(input);

                    if (hash.StartsWith("00000"))
                    {
                        Console.WriteLine(input + ", " + hash);
                        counter++;
                        break;
                    }
                    counter++;
                }
                p[i] = hash.Substring(5, 1); 
            }
            
            for (int j = 0; j < 8; j++)
            {
                Console.Write(p[j]);
            }
        }
        
        private static string CreateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
