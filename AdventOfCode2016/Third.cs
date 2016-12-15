using AdventOfCode.Helpers;
using System;
using System.IO;

namespace AdventOfCode
{
    public class Third
    {        
        public static void Run()
        {
            int numberOfValidTrianglesA = 0;
            int numberOfValidTrianglesB = 0;

            //using (WebClient client = new WebClient())
            //{
            //    string s = client.DownloadString("http://adventofcode.com/2016/day/3/input");
            //}

            string line;
            StreamReader file = new StreamReader(@".\data\triangles.txt");

            int i = 0;
            Triangle[] triangles = new Triangle[3];

            while ((line = file.ReadLine()) != null)
            {    
                int a = int.Parse(line.Substring(0, 5).Trim());
                int b = int.Parse(line.Substring(5, 5).Trim());
                int c = int.Parse(line.Substring(10, 5).Trim());

                Triangle t = new Triangle(a, b, c);

                if (t.IsValid())
                {
                    numberOfValidTrianglesA++;
                }

                triangles[i] = t;

                if (i == 2)
                {
                    Triangle tr = new Triangle(triangles[0].A, triangles[1].A, triangles[2].A);
                    if (tr.IsValid())
                    {
                        numberOfValidTrianglesB++;
                    }
                    tr = new Triangle(triangles[0].B, triangles[1].B, triangles[2].B);
                    if (tr.IsValid())
                    {
                        numberOfValidTrianglesB++;
                    }
                    tr = new Triangle(triangles[0].C, triangles[1].C, triangles[2].C);
                    if (tr.IsValid())
                    {
                        numberOfValidTrianglesB++;
                    }
                    i = 0;
                }
                else
                {
                    i++;
                }
            }

            file.Close();

            Console.WriteLine("NumberOfValidTrianglesA: " + numberOfValidTrianglesA);
            Console.WriteLine("NumberOfValidTrianglesB: " + numberOfValidTrianglesB);
        }
    }
}