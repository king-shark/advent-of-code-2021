using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdventCalendar
{
    public class LanternFishStudy
    {
        public Int64[] startData { get; set; }
        public Int64[] breedingData { get; set; }
        //public List<int> lanternFishCollection { get; set; }
        public int days; //number of days in the study
        public int breedingcycle { get { return 6; } }
        public int newbornOffset { get { return 2; } }
        public LanternFishStudy(string filename, int breedingCycle, int newbornOffset)
        {
            int arrLength = breedingCycle + newbornOffset;
            startData = new Int64[arrLength];
            breedingData = new Int64[arrLength];
            string line;
            StreamReader reader = File.OpenText(filename);
            while ((line = reader.ReadLine()) != null)
            {
                string[] lineArray = line.Split(',');
                foreach (string number in lineArray)
                {
                    startData[Int32.Parse(number)]++;                }
            }
        }
        public void UpdateFish(int days)
        {
            Array.Copy(startData, breedingData, startData.Length);
            for (int i = 0; i < days; i++)
            {
                Int64 dayZeroFish = breedingData[0];
                Array.Copy(breedingData, 1, breedingData, 0, breedingData.Length - 1);
                breedingData[breedingcycle + newbornOffset] = dayZeroFish;
                breedingData[breedingcycle] += dayZeroFish;
            }
        }
        public Int64 GetTotal()
        {
            Int64 sum = 0;
            foreach (Int64 fish in breedingData)
            {
                sum += fish;
            }
            return sum;
        }
    }
}
