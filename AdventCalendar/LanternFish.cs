using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventCalendar
{
    public class LanternFishStudy
    {
        public Dictionary<int, Int64> lanternFishCollection { get; set; }
        //public List<int> lanternFishCollection { get; set; }
        public int days; //number of days in the study
        public int breedingcycle { get { return 6; } }
        public int newbornOffset { get { return 2; } }
        public LanternFishStudy(Dictionary<int, Int64> fish, int days)
        {
            lanternFishCollection = fish;
            for (int i = breedingcycle + newbornOffset; i >= 0; i--)
            {
                if (!lanternFishCollection.ContainsKey(i))
                {
                    lanternFishCollection.Add(i, 0);
                }
            }
            this.days = days;
            for (int i = 0; i < days; i++)
            {
                UpdateFish();
            }
        }
        public void UpdateFish()
        {
            Dictionary<int, Int64> temp = new Dictionary<int, Int64>();
            temp = new Dictionary<int, Int64>(lanternFishCollection);
            Int64 dayZeroFish = lanternFishCollection[0];
            for (int key = breedingcycle + newbornOffset - 1; key >= 0; key--)
            {

                temp[key] = lanternFishCollection[key + 1];
            }
            temp[breedingcycle + newbornOffset] = 0;
            lanternFishCollection = temp;
            if (dayZeroFish > 0) { AddFish(dayZeroFish); }
        }
        public void AddFish(Int64 fish)
        {
            lanternFishCollection[breedingcycle + newbornOffset] = fish;
            lanternFishCollection[breedingcycle] += fish;
        }
        public Int64 GetTotal()
        {
            Int64 sum = 0;
            foreach (int key in lanternFishCollection.Keys)
            {
                sum += lanternFishCollection[key];
            }
            return sum;
        }
    }
}
