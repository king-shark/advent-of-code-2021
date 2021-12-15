using System;
using System.Collections.Generic;
using System.IO;
namespace AdventCalendar
{
    public class NavigationSubsystem
    {
        Stack<char> input;
        Dictionary<char, long> points;
        Dictionary<char, char> keyValues;
        
        List<long> scores;
        public long syntaxErrorScore;
        
        public NavigationSubsystem(string filename)
        {
            bool isNotCorrupt;
            syntaxErrorScore = 0;
            scores = new List<long>();
            DictionaryInit();
            string line;
            StreamReader reader = File.OpenText(filename);
            long total;
            while ((line = reader.ReadLine()) != null)
            {
                input = new Stack<char>();
                isNotCorrupt = true;
                for (int i = 0; i < line.ToCharArray().Length && isNotCorrupt; i++)
                {
                    char c = line[i];
                    if (keyValues.ContainsKey(c))
                    {
                        if (input.Peek() == keyValues[c])
                        {
                            input.Pop();
                        }
                        else { syntaxErrorScore += points[c];  isNotCorrupt = false; }
                    }
                    else input.Push(c);
                }
                if (isNotCorrupt)
                {
                    total = 0;
                    do
                    {
                        total = total * 5;
                        total = total + points[input.Peek()];
                        input.Pop();
                    } while (input.Count > 0);
                    scores.Add(total);
                }
            }
        }
        public void DictionaryInit()
        {
            points = new Dictionary<char, long>();
            keyValues = new Dictionary<char, char>();
            points.Add(')', 3);
            points.Add(']', 57);
            points.Add('}', 1197);
            points.Add('>', 25137);
            points.Add('(', 1);
            points.Add('[', 2);
            points.Add('{', 3);
            points.Add('<', 4);
            keyValues.Add(')', '(');
            keyValues.Add(']', '[');
            keyValues.Add('>', '<');
            keyValues.Add('}', '{');
            
        }
        public long GetAutocompleteScore()
        {
            scores.Sort();
            return scores[(int)Math.Floor((decimal)scores.Count / 2)];
        }
    }
}
