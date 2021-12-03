using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime;

namespace AdventCalendar
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> puzzleInput1 = GetPuzzleData("AdventPuzzleInput1.txt");
            Debug.WriteLine("Day 1: The answer you seek is " + AdventCalenderPuzzleOne(puzzleInput1, 1) + ".");
            Debug.WriteLine("Day 1: The answer to puzzle two is " + AdventCalenderPuzzleOne(puzzleInput1, 3) + ".");

            List<FlightInstruction> puzzleInput2 = GetPuzzleDataTwo("AdventPuzzleInput2.txt");
            Debug.WriteLine("Day 2: This is a tricky one. But for puzzle three, the answer be: " + AdventCalenderPuzzleTwo(puzzleInput2, false));
            Debug.WriteLine("Day 2: Well, turns out that last answer was rubbish. We needed more data! I now calculate our current position to be: " + AdventCalenderPuzzleTwo(puzzleInput2, true));

            List<string> puzzleInput3 = GetPuzzleData("AdventPuzzleInput3.txt");
            Debug.WriteLine("Day 3: Moving on to Day 3, Puzzle number one's data input says that the power consumption of the submarine is: " + AdventCalendarPuzzleFunctionFive(puzzleInput3));
            Debug.WriteLine("Day 3: Furthermore, based on my exquisite calculations, the life support rating of our submarine is: " + AdventCalendarPuzzleFunctionSix(puzzleInput3));
        }

        static int AdventCalendarPuzzleFunctionFive(List<string> list)
        {
            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
            string gammaStr = "", epsilonStr = "";
            
            foreach (var item in list)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    if (keyValuePairs.ContainsKey(i))
                    {
                        keyValuePairs[i] = keyValuePairs[i] + Int32.Parse(item.Substring(i,1));
                    }
                    else { keyValuePairs.Add(i, Int32.Parse(item.Substring(i, 1))); }
                }                
            }
            for (int i = 0; i < keyValuePairs.Count; i++)
            {
                //Debug.Print("Position {0} Count: {1}", i, keyValuePairs[i]);
                if (keyValuePairs[i] > list.Count/2) { gammaStr = gammaStr + "1"; epsilonStr = epsilonStr + "0"; }
                else { gammaStr = gammaStr + "0"; epsilonStr = epsilonStr + "1"; }
            }
            Debug.Print("Gamma: {0} Epsilon: {1}", gammaStr, epsilonStr);
            Debug.Print("Gamma Decimal: {0} Epsilon Decimal: {1}", Convert.ToInt32(gammaStr, 2), Convert.ToInt32(epsilonStr, 2));
            return Convert.ToInt32(gammaStr, 2) * Convert.ToInt32(epsilonStr, 2);
        }
        static string GetOxygenString(List<string> oxygenList)
        {
            int i = 0;
            do
            {
                List<string> list1 = oxygenList.Where(o => o.Substring(i, 1).Equals("1")).ToList(); 
                List<string> list2 = oxygenList.Where(o => o.Substring(i, 1).Equals("0")).ToList();
                if (list1.Count >= list2.Count) { oxygenList = list1; }
                else { oxygenList = list2; }
                i++;
            } while (oxygenList.Count > 1 && i < oxygenList[0].Length);

            return oxygenList[0];
        }
        static string GetCo2String(List<string> co2List)
        {
            int i = 0;
            do
            {
                List<string> list1 = co2List.Where(o => o.Substring(i, 1).Equals("0")).ToList();
                List<string> list2 = co2List.Where(o => o.Substring(i, 1).Equals("1")).ToList();
                if (list1.Count <= list2.Count) { co2List = list1; }
                else { co2List = list2; }
                i++;
            } while (co2List.Count > 1 && i < co2List[0].Length);
            return co2List[0];
        }

        static int AdventCalendarPuzzleFunctionSix(List<string> list)
        {
            string oxygen = GetOxygenString(list);
            string co2 = GetCo2String(list);
            
            Debug.Print("O2: {0} CO2: {1}", oxygen, co2);
            Debug.Print("O2 Decimal: {0} Co2 Decimal: {1}", Convert.ToInt32(oxygen, 2), Convert.ToInt32(co2, 2));
            return Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2);
        }

        static List<string> GetPuzzleData(string filename)
        {
            List<string> list = new List<string>();
            string line;
            StreamReader reader = File.OpenText(filename);

            while ((line = reader.ReadLine()) != null)
            {
                list.Add(line);
            }
            return list;
        }

        /// <summary>
        /// Puzzle 2 Stuff
        /// </summary>
        class FlightInstruction
        {
            public string direction { get; set; }
            public int value { get; set; }
        }
        static List<FlightInstruction> GetPuzzleDataTwo(string filename)
        {
            List<FlightInstruction> flightInstructions = new List<FlightInstruction>();
            string line;
            StreamReader reader = File.OpenText(filename);
            while((line = reader.ReadLine()) != null)
            {
                string[] lineArray = line.Split(' ');
                FlightInstruction instruction = new FlightInstruction
                {
                    direction = lineArray[0],
                    value = Int32.Parse(lineArray[1])
                };
                flightInstructions.Add(instruction);
            }
            return flightInstructions;
        }

        static int AdventCalenderPuzzleTwo(List<FlightInstruction> flightInstructions, bool aiming)
        {
            int horizontalPos = 0, depth = 0, aim = 0;

            foreach (var instruction in flightInstructions)
            {
                switch (instruction.direction)
                {
                    case "forward":
                        horizontalPos += instruction.value;
                        if (aiming) { depth += aim * instruction.value; }
                        break;
                    case "down":
                        if (aiming) { aim += instruction.value; }
                        else { depth += instruction.value; }
                        break;
                    case "up":
                        if (aiming) { aim -= instruction.value; }
                        else { depth -= instruction.value; }
                        break;
                    default:
                        break;
                }
            }
            return horizontalPos * depth;
        }

        /// <summary>
        /// Puzzle 1 Stuff
        /// </summary>
        /// <param name="list"></param>
        /// <param name="divider"></param>
        /// <returns></returns>
        static int AdventCalenderPuzzleOne(List<string> list, int divider)
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
