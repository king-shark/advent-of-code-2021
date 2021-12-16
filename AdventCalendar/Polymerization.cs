using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace AdventCalendar
{
    public class Polymerization
    {
        Dictionary<string, string> codex = new Dictionary<string, string>();
        Dictionary<string, long> nodePairs = new Dictionary<string, long>();
        Dictionary<string, long> nodeCounts { get; set; }

        public Polymerization(string filename, int steps)
        {
            string line;
            StreamReader reader = File.OpenText(filename);
            nodeCounts = new Dictionary<string, long>();
            line = reader.ReadLine();
            for (int i = 0; i < line.Length - 1; i++)
            {
                string temp = line.Substring(i, 2);
                if (nodePairs.ContainsKey(temp)) { nodePairs[temp]++; }
                else { nodePairs.Add(temp, 1); }
            }
            for (int i = 0; i < line.Length; i++)
            {
                UpdateCountDictionary(line.Substring(i, 1), 1);
            }
            while ((line = reader.ReadLine()) != null)
            {
                string[] input = line.Split(" -> ");
                codex.Add(input[0], input[1]);
            }
            for (int i = 1; i <= steps; i++)
            {
                UpdateNodePairs();
            }
        }
        
        public void UpdateNodePairs()
        {
            Dictionary<string, long> temp = new Dictionary<string, long>();
            foreach (string key in nodePairs.Keys)
            {
                string newPair = key.Substring(0, 1) + codex[key];
                if (temp.ContainsKey(newPair)) { temp[newPair] += nodePairs[key]; }
                else { temp.Add(newPair, nodePairs[key]); }
                newPair = codex[key] + key.Substring(1, 1);
                if (temp.ContainsKey(newPair)) { temp[newPair] += nodePairs[key]; }
                else { temp.Add(newPair, nodePairs[key]); }
                UpdateCountDictionary(codex[key], nodePairs[key]);
            }
            nodePairs = temp;
        }
        public void UpdateCountDictionary(string s, long value)
        {
            if (nodeCounts.ContainsKey(s)) { nodeCounts[s] += value; }
            else { nodeCounts.Add(s.Substring(0, 1), value); }

        }
        public long GetPuzzleAnswer()
        {
            return nodeCounts.Values.Max() - nodeCounts.Values.Min();
        }
    }

}
