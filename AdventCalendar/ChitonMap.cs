using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCalendar
{
    public class ChitonMap
    {
        int[,] nodes = null;
        public MapNode DestinationNode;
        public int RiskLevel = 0;
        List<MapNode> VisitedNodes = new List<MapNode>();
        List<MapNode> UnvisitedNodes = new List<MapNode>();
        public ChitonMap(string filename, int multiplier)
        {
            MapNode node;
            string[] input = File.ReadAllLines(filename);
            int width;
            int height;
            int i = 0;
            foreach (string s in input)
            {
                width = s.Length;
                height = input.Length;
                //if (nodes == null) { nodes = new int[width * multiplier, height * multiplier]; }
                int j = 0;
                foreach (var c in s.ToArray())
                {
                    for (int y = 0; y < multiplier; y++)
                    {
                        for (int x = 0; x < multiplier; x++)
                        {
                            int val = Int32.Parse(c.ToString()) + x + y;
                            node = new MapNode(j + x * width, i + y * height, val > 9 ? val - 9 : val, ((width * multiplier - 1) - (j + x * width)) + ((height * multiplier - 1) - (i + y * height)));
                            if (j == 0 && i == 0 && x == 0 && y == 0)
                            {
                                node.TentativeDistance = 0;
                                node.DistanceToEnd = 0;
                            }
                            else { node.TentativeDistance = Int32.MaxValue; }
                            UnvisitedNodes.Add(node);
                            if (j == width - 1 && i == height - 1 && y == multiplier - 1 && x == multiplier - 1)
                            {
                                DestinationNode = node;
                            }
                        }
                    }
                    j++;
                }
                i++;
            }
            do
            {
                UnvisitedNodes = UnvisitedNodes.OrderBy(nd => nd.TentativeDistance + nd.DistanceToEnd).ToList();
                node = UnvisitedNodes.First();
                if (node != DestinationNode)
                {
                    Djikstra(node);
                    VisitedNodes.Add(node);
                    UnvisitedNodes.Remove(node);
                }
                else { break; }
            } while (UnvisitedNodes.Count > 0);
        }
        public void Djikstra(MapNode curNode)
        {
            List<MapNode> neighbors = GetNeighbors(curNode);
            foreach (MapNode neighbor in neighbors)
            {
                if (neighbor != null)
                {
                    neighbor.TentativeDistance = neighbor.TentativeDistance > curNode.TentativeDistance + neighbor.Risk ? curNode.TentativeDistance + neighbor.Risk : neighbor.TentativeDistance;
                }
            }
        }
        public List<MapNode> GetNeighbors(MapNode node)
        {
            List<MapNode> newList = new List<MapNode>();
            //if (UnvisitedNodes.FirstOrDefault(n => n.XY == node.x - 1 + "," + node.y) != null) newList.Add(UnvisitedNodes.Single(n => n.XY == node.x - 1 + "," + node.y));
            //if (UnvisitedNodes.FirstOrDefault(n => n.XY == node.x + "," + (node.y - 1)) != null) newList.Add(UnvisitedNodes.Single(n => n.XY == node.x + "," + (node.y - 1)));
            if (UnvisitedNodes.FirstOrDefault(n => n.XY == node.x + "," + (node.y + 1)) != null) newList.Add(UnvisitedNodes.Single(n => n.XY == node.x + "," + (node.y + 1)));
            if (UnvisitedNodes.FirstOrDefault(n => n.XY == node.x + 1 + "," + node.y) != null) newList.Add(UnvisitedNodes.Single(n => n.XY == node.x + 1 + "," + node.y));
            return newList;
        }
    }
    public class MapNode
    {
        public int Risk = 0;
        public int TentativeDistance = 0;
        public int DistanceToEnd = 0;
        public string XY = "";

        public int x
        {
            get
            {
                return Int32.Parse(XY.Split(',')[0]);
            }
        }
        public int y
        {
            get
            {
                return Int32.Parse(XY.Split(',')[1]);
            }
        }
        public MapNode(int x, int y, int risk, int dist)
        {
            XY = x + "," + y;
            Risk = risk;
            DistanceToEnd = dist;
        }
    }
}
