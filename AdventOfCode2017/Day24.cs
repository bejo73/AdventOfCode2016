using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    class Day24
    {
        static List<Component> components = new List<Component>();

        public static int partOneMax = 0;
        public static int partTwoMax = 0;
        public static int partTwoMaxLength = 0;
        public static int length = 0;

        public static void Run()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\24.txt");

            while ((line = file.ReadLine()) != null)
            {
                string[] ports = line.Split('/');
                components.Add(new Component(ports[0], ports[1]));
            }

            foreach (Component c in components.Where(p => p.IsStartComponent()))
            {
                c.setLeftPort(0);
                c.connected = true;
                c.count = c.port1 + c.port2;
                length = 1;
                Connect(c);
                c.connected = false;
            }

            Console.WriteLine("Day24 (1): " + partOneMax);
            Console.WriteLine("      (2): " + partTwoMax);
        }

        public static void Connect(Component c)
        {
            if (components.Any(n => n.CanConnect(c.rightPort) && n.connected == false))
            {
                foreach (Component comp in components.Where(n => n.CanConnect(c.rightPort) && n.connected == false))
                {
                    length++;
                    comp.count = comp.port1 + comp.port2 + c.count;
                    comp.setLeftPort(c.rightPort);
                    comp.connected = true;
                    Connect(comp);
                    comp.connected = false;
                    length--;
                }
            }
            else
            {
                // Part I
                if (c.count > partOneMax)
                {
                    partOneMax = c.count;
                }

                // Part II
                if (length > partTwoMaxLength)
                {
                    partTwoMaxLength = length;
                    partTwoMax = c.count;
                }
                else if (length == partTwoMaxLength)
                {
                    if (c.count > partTwoMax)
                    {
                        partTwoMax = c.count;
                    }
                }
            }
        }
    }

    class Component
    {
        public int count;
        public int leftPort;
        public int rightPort;
        public int port1;
        public int port2;
        public bool connected;

        public Component(string port1, string port2)
        {
            this.port1 = Int32.Parse(port1);
            this.port2 = Int32.Parse(port2);
        }

        public void setLeftPort(int port)
        {
            if (port1 == port)
            {
                leftPort = port1;
                rightPort = port2;
            }
            else
            {
                leftPort = port2;
                rightPort = port1;
            }
        }

        public bool CanConnect(int port)
        {
            bool result = false;
            if (port == port1 || port == port2)
            {
                result = true;
            }
            return result;
        }

        public bool IsStartComponent()
        {
            bool result = false;
            if (port1 == 0 || port2 == 0)
            {
                result = true;
            }
            return result;
        }
    }
}