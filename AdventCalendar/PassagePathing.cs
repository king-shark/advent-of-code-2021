using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCalendar
{
    public class PassagePathing
    {
        List<Node> nodes;
        bool hasTraversedSmallNodeTwice = false;
        Stack<string> Path = new Stack<string>();
        public int PathCount = 0;
        public int PathCount2 = 0;
        public PassagePathing(string filename)
        {
            nodes = new List<Node>();
            
            string line;
            StreamReader reader = File.OpenText(filename);
            while ((line = reader.ReadLine()) != null)
            {
                string[] input = line.Split('-');
                
                Node node1 = nodes.Where(n => n.Name == input[0]).FirstOrDefault();
                if (node1 == null)
                {
                    node1 = new Node(input[0], input[0].All(char.IsUpper));
                    nodes.Add(node1);
                }
                Node node2 = nodes.Where(n => n.Name == input[1]).FirstOrDefault();
                if (node2 == null)
                {
                    node2 = new Node(input[1], input[1].All(char.IsUpper));
                    nodes.Add(node2);
                }
                node1.Children.Add(node2);
                node2.Children.Add(node1);
            }
            GetRoutes(nodes.Where(n => n.Name == "start").First());
            Path = new Stack<string>();
            GetRoutes2(nodes.Where(n => n.Name == "start").First(), hasTraversedSmallNodeTwice);

        }
        public void GetRoutes(Node node)
        {
            Path.Push(node.Name);
            foreach (Node n in node.Children)
            {
                if (n.Name != "start")
                {
                    if (n.Name == "end") { PathCount++; }
                    else if (n.IsBig == true)
                    {
                        GetRoutes(n);
                    }
                    else if (Path.Contains(n.Name) == false && n.IsBig == false)
                    {
                        GetRoutes(n);
                    }
                }
            }
            Path.Pop();
        }
        public void GetRoutes2(Node node, bool test)
        {
            Path.Push(node.Name);
            foreach (Node n in node.Children)
            {
                if (n.Name != "start")
                {
                    if (n.Name == "end") { PathCount2++; }
                    else if (n.IsBig == true)
                    {
                        GetRoutes2(n, test);
                    }
                    else if (Path.Contains(n.Name) == false && n.IsBig == false)
                    {
                        GetRoutes2(n, test);
                    }
                    else if (Path.Contains(n.Name) == true && !test)
                    {
                        GetRoutes2(n, true);
                    }
                }
            }
            Path.Pop();
        }
    }

    public class Node
    {
        public string Name;
        public bool IsBig;
        public List<Node> Children;

        public Node(string name, bool big)
        {
            Name = name;
            IsBig = big;
            Children = new List<Node>();
        }
    }
}
