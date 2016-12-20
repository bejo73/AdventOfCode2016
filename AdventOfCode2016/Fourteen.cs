using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Pad
    {
        public Char Character { get; set; }
        public int Index3 { get; set; }
        public int Index5 { get; set; }
        public bool Key { get; set; }
    }

    class Fourteen
    {
        public static void Run()
        {

            List<Pad> pads = new List<Pad>();
            List<Pad> pks = new List<Pad>();
            int tt = 0;
            int uu = 0;
            int oo = 0;
            int counter = 0;
            int keys = 0;
            string hash;

            //string salt = "abc";
            //string salt = "qzyelonm";
            string salt = "yjdafjpo";

            //int m5 = 0;
            //int m3 = 0;

            while (true)
            {
                
                hash = CreateMD5(salt + counter);

                // B
                //hash = create2016Hash(hash);

                /*
                MatchCollection matches = Regex.Matches(hash, @"(.)\1\1");

                for (int i = 0; i < matches.Count; i++)
                {
                    m5++;
                    
                    char c = Char.Parse((matches[i].Groups[1].Value).Substring(0, 1));

                    if (pads.Any(p => p.Character == c && p.Index3 == counter))
                    {
                        Console.WriteLine("Ignore 1");
                    }
                    else
                    {
                        pads.Add(new Pad() { Character = c, Index3 = counter });
                    }
                }

                matches = Regex.Matches(hash, @"(.)\1\1\1\1");

                for (int i = 0; i < matches.Count; i++)
                {
                    m3++;

                    
                    char c = Char.Parse((matches[i].Groups[1].Value).Substring(0, 1));
                    Console.WriteLine(c.ToString() + ", " + counter + ", " + hash + ", " + keys);

                    if (pads.Any(p => p.Character == c && p.Index5 == counter))
                    {
                        Console.WriteLine("Ignore 2");
                    }
                    else
                    {
                        Pad mp = pads.Find(sp => sp.Character == c);
                        if (mp != null)
                        {
                            Console.WriteLine(c.ToString() + ", " + (counter - mp.Index3) + tt++", " + mp.Index3 + ", " + counter + ", " + hash + ", " + keys);
                            keys++;
                            mp.Index5 = counter;
                            mp.Key = true;
                            pads2.Add(mp);
                            pads.Remove(mp);
                        }
                    }
                    
                }
                */

                Match m3 = Regex.Match(hash, @"(.)\1\1");
                Match m5 = Regex.Match(hash, @"(.)\1\1\1\1");

                
                if (m5.Success)
                {
                    tt++;
                    char c = Char.Parse((m5.Groups[1].Value).Substring(0, 1));
                    //Console.WriteLine(c.ToString() + ", " + counter + ", " + hash + ", ");

                    if (pads.Any(jp => jp.Character == c))
                    {

                         Pad pe = pads.Find(hg => hg.Character == c);
                         if (pe == null)
                         {
                            Pad pg = new Pad() { Character = c, Index3 = counter };
                                pads.Add(pg);
                            //Console.WriteLine(c.ToString() + ", " + (counter - p.Index3) + ", " + p.Index3 + ", " + counter + ", " + hash + ", " + keys);
                            //pads.Remove(p);
                            //keys++;
                            //break;
                        }
                        

                         foreach (Pad p in pads.Where(mp => mp.Character == c && !mp.Key))
                         {
                             Console.WriteLine(c.ToString() + ", " + (counter - p.Index3) + ", " + p.Index3 + ", " + counter + ", " + hash + ", " + keys);
                            
                           
                            p.Index5 = counter;
                            p.Key = true;
                            pks.Add(p);
                            //pads.Remove(p);
                            keys++;
                            if (keys == 64)
                            {
                                Console.WriteLine("Counter : " + counter);
                                break;
                            }



                            //break;
                        }
                     }
                    //else
                    //{
                    //    Console.WriteLine("fkdsljfkgldfjglkdjfglkdjfglkjfdglkdjf");
                    //    Pad p = new Pad() { Character = c, Index3 = counter };
                    //    pads.Add(p);
                    //}
                }



                if (m3.Success)
                {
                    uu++;
                    char c = Char.Parse((m3.Groups[1].Value).Substring(0, 1));
                    //Console.WriteLine(c.ToString() + ", " + counter + ", " + hash + ", " + keys);

                    Pad p = new Pad() { Character = c, Index3 = counter };
                    pads.Add(p);
                }








                if (keys == 64)
               {
                    Console.WriteLine("Counter : " + counter);
                    break;
                }
                   

                pads.RemoveAll(p => p.Index3 <= (counter - 1000));
                counter++;

                if (counter == 32900)
                {
                    Console.WriteLine(uu + ", " + tt + ", " + oo + ", " + counter);
                    break;
                 }

                oo++;
            }

        }

        public static void Run2()
        {
            List<Pad> pads = new List<Pad>();

            List<Pad> pks = new List<Pad>();
            int m5Hit = 0;
            int m3Hit = 0;
            int oo = 0;
            int counter = 0;
            int keys = 0;
            string hash;

            //string salt = "abc";
            string salt = "qzyelonm";
            //string salt = "yjdafjpo";

            //int m5 = 0;
            //int m3 = 0;

            while (true)
            {

                hash = CreateMD5(salt + counter);

                // B
                //hash = create2016Hash(hash);

                /*
                MatchCollection matches = Regex.Matches(hash, @"(.)\1\1");

                for (int i = 0; i < matches.Count; i++)
                {
                    m5++;
                    
                    char c = Char.Parse((matches[i].Groups[1].Value).Substring(0, 1));

                    if (pads.Any(p => p.Character == c && p.Index3 == counter))
                    {
                        Console.WriteLine("Ignore 1");
                    }
                    else
                    {
                        pads.Add(new Pad() { Character = c, Index3 = counter });
                    }
                }

                matches = Regex.Matches(hash, @"(.)\1\1\1\1");

                for (int i = 0; i < matches.Count; i++)
                {
                    m3++;

                    
                    char c = Char.Parse((matches[i].Groups[1].Value).Substring(0, 1));
                    Console.WriteLine(c.ToString() + ", " + counter + ", " + hash + ", " + keys);

                    if (pads.Any(p => p.Character == c && p.Index5 == counter))
                    {
                        Console.WriteLine("Ignore 2");
                    }
                    else
                    {
                        Pad mp = pads.Find(sp => sp.Character == c);
                        if (mp != null)
                        {
                            Console.WriteLine(c.ToString() + ", " + (counter - mp.Index3) + tt++", " + mp.Index3 + ", " + counter + ", " + hash + ", " + keys);
                            keys++;
                            mp.Index5 = counter;
                            mp.Key = true;
                            pads2.Add(mp);
                            pads.Remove(mp);
                        }
                    }
                    
                }
                */

                Match m3 = Regex.Match(hash, @"(.)\1\1");
                Match m5 = Regex.Match(hash, @"(.)\1\1\1\1");

                if (m3.Success)
                {
                    m3Hit++;
                    char c = Char.Parse((m3.Groups[1].Value).Substring(0, 1));
                    Pad p = new Pad() { Character = c, Index3 = counter };
                    pads.Add(p);
                }

                if (m3.Success && m5.Success)
                {
                    
                }

                if (m5.Success)
                {
                    m5Hit++;
                    char c = Char.Parse((m5.Groups[1].Value).Substring(0, 1));


                    if (pads.Any(jp => jp.Character == c))
                    {

                        Pad pe = pads.Find(hg => hg.Character == c);
                        if (pe == null)
                        {
                            Pad pg = new Pad() { Character = c, Index3 = counter };
                            pads.Add(pg);
                            //Console.WriteLine(c.ToString() + ", " + (counter - p.Index3) + ", " + p.Index3 + ", " + counter + ", " + hash + ", " + keys);
                            //pads.Remove(p);
                            //keys++;
                            //break;
                        }


                        foreach (Pad p in pads.Where(mp => mp.Character == c && !mp.Key))
                        {
                            Console.WriteLine(c.ToString() + ", " + (counter - p.Index3) + ", " + p.Index3 + ", " + counter + ", " + hash + ", " + keys);


                            p.Index5 = counter;
                            p.Key = true;
                            pks.Add(p);
                            //pads.Remove(p);
                            keys++;
                            if (keys == 64)
                            {
                                Console.WriteLine("Counter : " + counter);
                                //break;
                            }



                            //break;
                        }
                    }
                    //else
                    //{
                    //    Console.WriteLine("fkdsljfkgldfjglkdjfglkdjfglkjfdglkdjf");
                    //    Pad p = new Pad() { Character = c, Index3 = counter };
                    //    pads.Add(p);
                    //}
                }












                if (keys == 64)
                {
                    Console.WriteLine("Counter : " + counter);
                    //break;
                }


                //pads.RemoveAll(p => p.Index3 <= (counter - 1000));
                counter++;

                if (counter == 16000)
                {
                    Console.WriteLine(m3Hit + ", " + m5Hit + ", " + oo + ", " + counter);
                    break;
                }

                oo++;
            }
        }

        private static string create2016Hash(string hash)
        {
            for (int i = 0; i < 2016; i++)
            {
                hash = CreateMD5(hash);
            }
            return hash;
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
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }


    }
}