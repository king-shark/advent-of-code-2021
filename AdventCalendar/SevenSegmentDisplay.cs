using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventCalendar
{
    public class SevenSegmentDisplay
    {
        List<DisplayEntry> DisplayEntries = new List<DisplayEntry>();
        public SevenSegmentDisplay(string filename)
        {
            string line;
            StreamReader reader = File.OpenText(filename);
            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split(" | ");
                DisplayEntries.Add(new DisplayEntry(data[0].Split(' ').ToList(), data[1].Split(' ').ToList()));
            }
        }
        public int GetEasyNumberCount()
        {
            int count = 0;
            foreach (DisplayEntry entry in DisplayEntries)
            {
                foreach (string str in entry.Outputs)
                {
                    switch (str.Length)
                    {
                        case 2:
                        case 4:
                        case 3:
                        case 7:
                            count += 1;
                            break;
                        default:
                            break;
                    }
                }
            }
            return count;
        }
        public int GetOutputSum()
        {
            int sum = 0;
            foreach (DisplayEntry entry in DisplayEntries)
            {
                sum += entry.TranslateOutput();
            }
            return sum;
        }
    }

    public class DisplayEntry
    {
        public List<string> Inputs { get; set; }
        public List<string> Outputs { get; set; }
        //List<char[]> codex = new List<char[]>();
        Dictionary<string, int> Codex = new Dictionary<string, int>();

        public DisplayEntry(List<string> input, List<string> output)
        {
            Inputs = new List<string>(input);
            Inputs = Inputs.OrderBy(i => i.Length).ToList();
            Outputs = new List<string>(output);
        }
        public int TranslateOutput()
        {
            string output = "";
            UpdateCodex();
            foreach (string str in Outputs)
            {
                output += Codex[SortString(str)];
                
            }
            return Int32.Parse(output);
        }
        public string SortString(string str)
        {
            var s = str.ToList();
            s.Sort();
            return new string(s.ToArray());
        }
        public void UpdateCodex()
        {
            string[] temp = new string[10];
            temp[1] = Inputs.Where(i => i.Length == 2).Single();
            temp[4] = Inputs.Where(i => i.Length == 4).Single();
            temp[7] = Inputs.Where(i => i.Length == 3).Single();
            temp[8] = Inputs.Where(i => i.Length == 7).Single();
            string[] zerosixnine = Inputs.Where(i => i.Length == 6).ToArray();
            foreach (var str in zerosixnine)
            {
                if (str.Contains(temp[1][0]) && str.Contains(temp[1][1]))
                {
                    //number is zero or nine
                    if (str.Contains(temp[4][0]) &&
                        str.Contains(temp[4][1]) &&
                        str.Contains(temp[4][2]) &&
                        str.Contains(temp[4][3]))
                    {
                        temp[9] = str;
                    }
                    else { temp[0] = str; }
                }
                else
                {
                    //number is six
                    temp[6] = str;
                }
            }
            string[] twothreefive = Inputs.Where(i => i.Length == 5).ToArray();
            foreach (var str in twothreefive)
            {
                if (str.Contains(temp[1][0]) &&
                    str.Contains(temp[1][1]))
                {
                    temp[3] = str;
                }
                else if (temp[6].Contains(str[0]) &&
                    temp[6].Contains(str[1]) &&
                    temp[6].Contains(str[2]) &&
                    temp[6].Contains(str[3]) &&
                    temp[6].Contains(str[4]))
                {
                    temp[5] = str;
                }
                else { temp[2] = str; }
            }
            for (int i = 0; i < temp.Length; i++)
            { Codex.Add(SortString(temp[i]), i); }
        }
    }
}
