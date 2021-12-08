using System.Collections.Generic;
using System.Diagnostics;

namespace AdventCalendar
{
    class Program
    {
        static void Main(string[] args)
        {
            SonarSweep puzzle1 = new SonarSweep("AdventPuzzleInput1.txt");
            Debug.WriteLine("Day 1: The answer you seek is " + puzzle1.AnalyzeSweepData(1) + ".");
            Debug.WriteLine("Day 1: The answer to puzzle two is " + puzzle1.AnalyzeSweepData(3) + ".");

            NavigationSystems puzzle2 = new NavigationSystems("AdventPuzzleInput2.txt");
            Debug.WriteLine("Day 2: This is a tricky one. But for puzzle three, the answer be: " + puzzle2.CalculateFlightPath(false));
            Debug.WriteLine("Day 2: Well, turns out that last answer was rubbish. We needed more data! I now calculate our current position to be: " + puzzle2.CalculateFlightPath(true));

            SubmarineDiagnostics puzzle3 = new SubmarineDiagnostics("AdventPuzzleInput3.txt");
            Debug.WriteLine("Day 3: Moving on to Day 3, Puzzle number one's data input says that the power consumption of the submarine is: " + puzzle3.PowerConsumption());
            Debug.WriteLine("Day 3: Furthermore, based on my exquisite calculations, the life support rating of our submarine is: " + puzzle3.LifeSupportRating());

            Bingo puzzle4 = new Bingo("AdventPuzzleInput4.txt",5,5);
            Debug.WriteLine("Day 4: Bingo with a squid then. Alright. If I am to win, I must pick the board with a final score of " + puzzle4.PlayBingo());
            Debug.WriteLine("Day 4: However, might it not be prudent to let the beast win? Perhaps I should pick the board guaranteed to lose. It would be the one with a final score of " + puzzle4.LoseBingo());

            GeoThermalVentMap puzzle5 = new GeoThermalVentMap("AdventPuzzleInput5.txt", 1000,1000, false);
            Debug.WriteLine("Day 5: Great, now there's geothermal vents. Well, no matter. If I comb the map data, it looks like there are only {0} points I need to avoid. Easy.", puzzle5.GetVentDensity(2));
            puzzle5 = new GeoThermalVentMap("AdventPuzzleInput5.txt", 1000, 1000, true);
            Debug.WriteLine("Day 5: Did I seriously forget to include diagonal vents?? Now it looks like there are {0} points I need to avoid. Only slightly less easy.", puzzle5.GetVentDensity(2));
            
            LanternFishStudy puzzle6 = new LanternFishStudy("AdventPuzzleInput6.txt", 7, 2);
            int days = 80;
            puzzle6.UpdateFish(days);
            Debug.WriteLine("Day 6: A swarm of lanternfish. Surely those sleigh keys have to be somewhere around here! I guess as long as I'm here, I better perform some data analytics on these lanternfishes. There sure do seem to be a lot of them. ");        
            Debug.WriteLine("Based on my calculations and assumptions about lanternfish breeding habits, I bet after {0} days, there will be {1} fish altogether.", days, puzzle6.GetTotal());
            days = 256;
            puzzle6.UpdateFish(days);
            Debug.WriteLine("Day 6: By Santa's Beard... If these fish were to find a way to acheive immortality, then after a mere {0} days, there would be {1} of them!", days, puzzle6.GetTotal());
            Debug.WriteLine("I must find Santa's sleigh keys so I can inform him of this potential threat to Christmas!");

            CrabSubmarine puzzle7 = new CrabSubmarine("AdventPuzzleInput7.txt");
            Debug.WriteLine("Day 7: It is difficult to concentrate over the sound of the alarms but I have them to thank for having avoided becoming lunch for a whale! These crabs in submarines seem to be trying to help me.");
            Debug.WriteLine("All I need to do is get them to move to the position where the sum is: {0}", puzzle7.GetMostFuelEfficientPosition());
            Debug.WriteLine("Day 7: The crabs are dissatisfied with my results. It looks like my assumptions about crab submarine fuel consumption were wrong.");
            Debug.WriteLine("After a brief update to my code, I have determined that the correct position to move to is: {0}", puzzle7.GetCrabMostFuelEfficientPosition());

            SevenSegmentDisplay puzzle8 = new SevenSegmentDisplay("AdventPuzzleInput8.txt");
            Debug.WriteLine("Day 8: Now the display is acting up. Let's see, I think I can translate the garbled output into something usable.");
            Debug.WriteLine("I think the number of times 1,4, 7 and 8 appear is {0}", puzzle8.GetEasyNumberCount());
            Debug.WriteLine("Day 8: I think I've cracked a way to read the output. If I'm right, the total of all the output digits should be {0}.", puzzle8.GetOutputSum());
        }
    }
}