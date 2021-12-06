using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventCalendar
{
    public class LanternFishStudy
    {
        public List<int> lanternFishCollection { get; set; }
        public int days; //number of days in the study
        public int breedingcycle { get { return 6; } }
        public int newbornOffset { get { return 2; } }
        public LanternFishStudy(List<int> fish, int days)
        {
            lanternFishCollection = fish;
            this.days = days;
            for (int i = 0; i < days; i++)
            {
                UpdateFish();
                Debug.Write("\rCalculating fish... Day " + i);
            }
        }
        public void UpdateFish()
        {
            List<int> newFish = new List<int>();
            for (int i = 0; i < lanternFishCollection.Count; i++)
            {
                if (lanternFishCollection[i] == 0)
                {
                    lanternFishCollection[i] = breedingcycle;
                    newFish.Add(breedingcycle + newbornOffset);
                }
                else if (lanternFishCollection[i] > 0) { lanternFishCollection[i]--; }
            }
            lanternFishCollection.AddRange(newFish);
        }

    }
}
