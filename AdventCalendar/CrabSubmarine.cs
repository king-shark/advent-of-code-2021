using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventCalendar
{
    public class CrabSubmarine
    {
        List<int> arr = new List<int>();
        public CrabSubmarine(string filename)
        {
            string line;
            StreamReader reader = File.OpenText(filename);
            while ((line = reader.ReadLine()) != null)
            {
                string[] lineArray = line.Split(',');
                
                foreach (string number in lineArray)
                {
                    Int32 num = Int32.Parse(number);
                    arr.Add(num);
                }
                arr.Sort();
            }
        }
        public int GetMostFuelEfficientPosition()
        {
            int index = (int)Math.Floor((decimal)arr.Count / 2);
            int input = arr[index];
            int sum = 0;
            foreach (int number in arr)
            {
                sum += Math.Abs(number - input);
            }
            return sum;
        }
        public int GetCrabMostFuelEfficientPosition()
        {
            int avg = (int)Math.Floor(arr.Average());
            int sum = 0;
            foreach (int number in arr)
            {
                int input = Math.Abs(number - avg);
                sum += input * (input + 1) / 2;
            }
            return sum;
        }
    }
}