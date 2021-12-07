using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventCalendar
{
    public class SonarSweep
    {
        static List<string> list = new List<string>();
        public SonarSweep(string filename)
        {
            
            string line;
            StreamReader reader = File.OpenText(filename);
            while ((line = reader.ReadLine()) != null)
            {
                list.Add(line);
            }
        }
        public int AnalyzeSweepData(int divider)
        {
            if (divider != 0 && divider < list.Count)
            {
                List<string> newList = new List<string>();
                if (divider > 1)
                {
                    for (int i = divider; i <= list.Count; i++)
                    {
                        int newInt = 0;
                        for (int x = i - 1; x >= i - divider; x--)
                        {
                            newInt += Int32.Parse(list[x]);
                        }
                        newList.Add(newInt.ToString());
                    }
                }
                else { newList = list; }
                int increaseCount = 0;
                for (int i = 1; i < newList.Count; i++)
                {
                    if (Int32.Parse(newList[i]) > Int32.Parse(newList[i - 1])) { increaseCount++; }
                }
                return increaseCount;
            }
            return 0;
        }
    }
}
