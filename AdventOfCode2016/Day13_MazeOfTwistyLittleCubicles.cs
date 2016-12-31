using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day13_MazeOfTwistyLittleCubicles
    {
        private static int favouriteNumber = 1352;
        private static int maxColumns = 50; // Columns
        private static int maxRows    = 45;  // Rows

        private const int COLUMNS = 1;
        private const int ROWS    = 0;

        private static List<Node> open = new List<Node>();
        private static List<Node> closed = new List<Node>();

        public static void Run()
        {
            char[,] room = CreateRoom(maxColumns, maxRows, favouriteNumber);
            PrintRoom(room);

            // Create starting and end points
            Node start = new Node { X = 1,  Y = 1 };
            Node end   = new Node { X = 31, Y = 39 };

            // Add to open list
            Node current = start;
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
                    int cost = Math.Abs(end.X - n.X) + Math.Abs(end.Y - n.Y);
                    if (cost < lowestCost)
                    {
                        lowestCost = cost;
                        current = n;
                    }
                }

                if (current.X == end.X && current.Y == end.Y)
                {
                    Console.WriteLine(CountPath(current));
                    break;
                }
            }
        }

        private static void AddAdjacentNode(Node current, int x, int y, ref List<Node> nodes, char[,] room)
        {
            if (x < 0) return;
            if (y < 0) return;

            if (room[y, x] != '#' && x >= 0 && y >= 0)
            {
                // Check if already in open or closed lists
                if (!open.Any(on => (on.X == x) && (on.Y == y)))
                {
                    if (!closed.Any(cn => (cn.X == x) && (cn.Y == y)))
                    {
                        Node adjacentNode = new Node { X = x, Y = y };
                        adjacentNode.ParentNode = current;
                        nodes.Add(adjacentNode);
                    }
                }
            }
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

        private static char[,] CreateRoom(int maxColumns, int maxRows, int number)
        {
            char[,] room = new char[maxRows, maxColumns];

            for (int x = 0; x < room.GetLength(COLUMNS); x++)
            {
                for (int y = 0; y < room.GetLength(ROWS); y++)
                {
                    int sum = (x * x) + (3 * x) + (2 * x * y) + y + (y * y);
                    sum = sum + number;

                    string binary = Convert.ToString(sum, 2);
                    int ones = binary.Count(c => c.Equals('1'));
                    if ((ones % 2) == 0)
                    {
                        room[y, x] = '.';
                    }
                    else
                    {
                        room[y, x] = '#';
                    }
                }
            }

            return room;
        }

        public static void RunB()
        {
            char[,] room = CreateRoom(maxColumns, maxRows, favouriteNumber);
            PrintRoom(room);

            // Create starting and end points
            Node start = new Node { X = 1, Y = 1 };
            Node end = new Node { X = 31, Y = 39 };

            // Add to open list
            Node current = start;
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
                    int cost = Math.Abs(end.X - n.X) + Math.Abs(end.Y - n.Y);
                    if (cost < lowestCost)
                    {
                        lowestCost = cost;
                        current = n;
                    }
                }

                if (CountPath(current) > 50)
                {
                    open.Remove(current);
                    current = open.FirstOrDefault();
                }

                if (open.Count == 0)
                {
                    Console.WriteLine(closed.Count);
                    break;
                } 
            }
        }

        private static int CountPath(Node node)
        {
            int steps = 0;
            while (true)
            {
                //Console.WriteLine("X={0}, Y={1}", current.X, current.Y);
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