using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
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

            Bingo bingoGame = GetPuzzleDataThree("AdventPuzzleInput4.txt",5,5);
            Debug.WriteLine("Day 4: Bingo with a squid then. Alright. If I am to win, I must pick the board with a final score of " + bingoGame.PlayBingo());
            Debug.WriteLine("Day 4: However, might it not be prudent to let the beast win? Perhaps I should pick the board guaranteed to lose. It would be the one with a final score of " + bingoGame.LoseBingo());

            GeoThermalVentMap ventMap = GetPuzzleDataFour("AdventPuzzleInput5.txt", false);
            Debug.WriteLine("Day 5: Great, now there's geothermal vents. Well, no matter. If I comb the map data, it looks like there are only {0} points I need to avoid. Easy.", ventMap.GetVentDensity(2));
            ventMap = GetPuzzleDataFour("AdventPuzzleInput5.txt", true);
            Debug.WriteLine("Day 5: Did I seriously forget to include diagonal vents?? Now it looks like there are {0} points I need to avoid. Only slightly less easy.", ventMap.GetVentDensity(2));


            //This solution works for part 1 but it is not scalable for part 2
            LanternFishStudy fishStudy = GetPuzzleDataFive("AdventPuzzleInput6.txt", 80);
            Debug.WriteLine("Day 6: A swarm of lanternfish. Surely those sleigh keys have to be somewhere around here! I guess as long as I'm here, better perform some data analytics on these lanternfishes. ");        
            Debug.WriteLine("Based on my calculations and assumptions about lanternfish breeding habits, I bet after {0} days, there will be {1} fish altogether.", fishStudy.days, fishStudy.lanternFishCollection.Count);
            //fishStudy = GetPuzzleDataFive("AdventPuzzleInput6.txt", 256);
            Debug.WriteLine("Day 6: By Santa's Beard... If these fish were to find a way to acheive immortality, then after a mere {0} days, there would be {1} of them!", fishStudy.days, fishStudy.lanternFishCollection.Count);
            Debug.WriteLine("I must find Santa's sleigh keys so I can inform him of this potential threat to Christmas!");

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
        static GeoThermalVentMap GetPuzzleDataFour(string filename, bool includeDiagonal)
        {
            List<Vent> vents = new List<Vent>();
            StreamReader reader = File.OpenText(filename);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                int x = 0;
                int y = 0;
                string[] lineArray = line.Split(" -> ");
                string[] point0 = lineArray[0].Split(',');
                string[] point1 = lineArray[1].Split(',');
                vents.Add(new Vent(new Point(Int32.Parse(point0[0]), Int32.Parse(point0[1])), new System.Drawing.Point(Int32.Parse(point1[0]), Int32.Parse(point1[1]))));
            }
            GeoThermalVentMap map = new GeoThermalVentMap(1000, 1000, vents, includeDiagonal);
            return map;
        }

        static LanternFishStudy GetPuzzleDataFive(string filename, int studyLength)
        {
            List<int> intList = new List<int>();
            string line;
            StreamReader reader = File.OpenText(filename);
            while ((line = reader.ReadLine()) != null)
            {
                string[] lineArray = line.Split(',');
                foreach (string number in lineArray)
                {
                    intList.Add(Int32.Parse(number));
                }
            }

            LanternFishStudy study = new LanternFishStudy(intList, studyLength);
            return study;
        }

        static Bingo GetPuzzleDataThree(string filename, int width, int height)
        {
            Bingo game = new Bingo();
            int[,] boardData = new int[width, height];
            List<int[]> rows = new List<int[]>();
            string line;
            int lineNumber = 1;
            StreamReader reader = File.OpenText(filename);
            while ((line = reader.ReadLine()) != null)
            {
                //read in called numbers
                if (lineNumber == 1)
                {
                    string[] lineArray = line.Split(',');
                    foreach (string item in lineArray)
                    {
                        game.calledNumbers.Add(Int32.Parse(item));
                    }
                }
                //start of board data
                if (lineNumber > 2)
                {
                    if (line != "")
                    {
                        string[] lineArray = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        int[] row = new int[width];
                        for (int i = 0; i < width; i++)
                        {
                            row[i] = Int32.Parse(lineArray[i]);
                        }
                        rows.Add(row);
                        if (rows.Count == height)
                        {
                            boardData = new int[width, height];
                            for (int y = 0; y < rows.Count; y++)
                            {
                                for (int x = 0; x < rows[y].Length; x++)
                                {
                                    boardData[x, y] = rows[y][x];
                                }
                            }
                            game.bingoBoards.Add(new BingoBoard(width, height, boardData));
                            rows = new List<int[]>();
                        }
                    }
                }
                lineNumber++;
            }
            return game;
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
