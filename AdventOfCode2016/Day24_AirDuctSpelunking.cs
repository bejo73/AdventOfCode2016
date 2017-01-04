using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace AdventOfCode
{
    class Day24_AirDuctSpelunking
    {
        private const int COLUMNS = 1;
        private const int ROWS = 0;

        private static List<Node> open = new List<Node>();
        private static List<Node> closed = new List<Node>();

        public static void Run()
        {
            char[,] room = CreateRoom();
            PrintRoom(room);

            List<Node> nodes = GetPointsOfInterest(room);
            List<Node> sorted = nodes.OrderBy(n => n.Id).ToList();

            foreach (Node n in sorted)
            {
                Console.WriteLine(n.X + "," + n.Y + ", Id=" + n.Id);
            }

            Node start = nodes.First(no => no.Id == '0');
            sorted.Remove(start);

            var v = GetPermutations(sorted, sorted.Count);

            Node current = start;
            Node end = null;

            open.Add(current);
            int lowest = Int32.MaxValue;

            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(1.0);

            foreach (var f in v)
            {
                int total = 0;

                foreach (Node i in f)
                {
                    end = i;
                    
                    Console.Write("{0} => {1}", current.Id, end.Id);

                    string key = "" + current.Id + end.Id;

                    //Get object from cache and return it, if its there
                    var val = (string)cache.Get(key);
                    if (val != null)
                    {
                        int intval = Int32.Parse(val);
                        Console.WriteLine(", steps: {0}", intval);
                        total = total + intval;
                    }
                    else
                    {
                        while (true)
                        {
                            // Find adjacent nodes
                            List<Node> adjacent = GetAdjacentNodes(current, room);

                            // Add to open list
                            open.AddRange(adjacent);

                            // Remove current node from open list and add to closed list
                            open.Remove(current);
                            closed.Add(current);

                            // Find next node with lowest cost
                            int lowestCost = Int32.MaxValue;
                            foreach (Node n in open)
                            {
                                int cost = Math.Abs(end.X - n.X) + Math.Abs(end.Y - n.Y) + n.Cost;
                                if (cost < lowestCost)
                                {
                                    lowestCost = cost;
                                    current = n;
                                }
                            }

                            if (current.X == end.X && current.Y == end.Y)
                            {
                                int steps = CountPath(current);

                                cache.Add(key, "" + steps, policy);

                                Console.WriteLine(", steps: {0}", steps);
                                total = total + steps;
                                break;
                            }
                        }
                    }    
                    
                    current = end;
                    current.ParentNode = null;
                    open.Clear();
                    closed.Clear();
                    open.Add(current);
                }

                /* Part II
                // Add Start -> End
                current = start;
                var valw = (string)cache.Get("" + current.Id + end.Id);
                if (valw != null)
                {
                    int intval = Int32.Parse(valw);
                    Console.WriteLine(", steps: {0}", intval);
                    total = total + intval;
                }
                else
                {
                    current.ParentNode = null;
                    open.Clear();
                    closed.Clear();
                    open.Add(current);
                    while (true)
                    {
                        // Find adjacent nodes
                        List<Node> adjacent = GetAdjacentNodes(current, room);

                        // Add to open list
                        open.AddRange(adjacent);

                        // Remove current node from open list and add to closed list
                        open.Remove(current);
                        closed.Add(current);

                        // Find next node with lowest cost
                        int lowestCost = Int32.MaxValue;
                        foreach (Node n in open)
                        {
                            int cost = Math.Abs(end.X - n.X) + Math.Abs(end.Y - n.Y) + n.Cost;
                            if (cost < lowestCost)
                            {
                                lowestCost = cost;
                                current = n;
                            }
                        }

                        if (current.X == end.X && current.Y == end.Y)
                        {
                            int steps = CountPath(current);

                            cache.Add("" + current.Id + end.Id, "" + steps, policy);

                            Console.WriteLine(", steps: {0}", steps);
                            total = total + steps;
                            break;
                        }
                    }
                }
                */

                current = start;
                current.ParentNode = null;
                open.Clear();
                closed.Clear();
                open.Add(current);

                Console.WriteLine("Total steps: " + total );

                if (total < lowest)
                {
                    lowest = total;
                }
            }

            Console.WriteLine("Lowest steps: " + lowest);
        }

        private static char[,] CreateRoom()
        {
            string line;
            StreamReader file = new StreamReader(@".\Data\24.txt");
            List<string> lines = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }

            int rows = lines.Count;
            int columns = lines[0].Length;

            char[,] room = new char[rows, columns];

            for (int x = 0; x < room.GetLength(COLUMNS); x++)
            {
                for (int y = 0; y < room.GetLength(ROWS); y++)
                {
                    room[y, x] = Char.Parse(lines[y].Substring(x, 1));
                }
            }

            return room;
        }

        private static void PrintRoom(char[,] room)
        {
            for (int i = 0; i < room.GetLength(ROWS); i++)
            {
                for (int j = 0; j < room.GetLength(COLUMNS); j++)
                {
                    Console.Write(room[i, j]);
                }
                Console.WriteLine();
            }
        }

        private static List<Node> GetPointsOfInterest(char[,] room)
        { 
            List<Node> nodes = new List<Node>();

            for (int i = 0; i < room.GetLength(ROWS); i++)
            {
                for (int j = 0; j < room.GetLength(COLUMNS); j++)
                {
                    if (char.IsDigit(room[i, j]))
                    {
                        nodes.Add(new Node() { X = j, Y = i, Id = room[i, j] });
                    }
                }
            }

            return nodes;
        }

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1)
            {
                return list.Select(t => new T[] { t });
            }

            return GetPermutations(list, length - 1).SelectMany(t => list.Where(o => !t.Contains(o)), (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        private static List<Node> GetAdjacentNodes(Node node, char[,] room)
        {
            List<Node> nodes = new List<Node>();

            // Up
            AddAdjacentNode(node, node.X, node.Y - 1, ref nodes, room);

            // Left
            AddAdjacentNode(node, node.X - 1, node.Y, ref nodes, room);

            // Down
            AddAdjacentNode(node, node.X, node.Y + 1, ref nodes, room);

            // Right
            AddAdjacentNode(node, node.X + 1, node.Y, ref nodes, room);

            return nodes;
        }

        private static void AddAdjacentNode(Node current, int x, int y, ref List<Node> nodes, char[,] room)
        {
            if ((x < 0) || (y < 0)) return;

            if (room[y, x] != '#' && x >= 0 && y >= 0)
            {
                // Check if already in open or closed lists
                if (!open.Any(on => (on.X == x) && (on.Y == y)))
                {
                    if (!closed.Any(cn => (cn.X == x) && (cn.Y == y)))
                    {
                        Node adjacentNode = new Node { X = x, Y = y, Cost = current.Cost + 1 };
                        adjacentNode.ParentNode = current;
                        nodes.Add(adjacentNode);
                    }
                }
            }
        }

        private static int CountPath(Node node)
        {
            int steps = 0;
            while (true)
            {
                //Console.WriteLine("X={0}, Y={1}", node.X, node.Y);
                if (node.ParentNode == null)
                {
                    break;
                }
                steps++;
                node = node.ParentNode;
            }
            return steps;
        }
    }
}