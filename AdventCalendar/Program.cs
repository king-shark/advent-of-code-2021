using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime;

namespace AdventCalendar
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> puzzleInput1 = GetPuzzleDataOne();
            List<int> sumList = AdventCalenderPuzzleFunctionTwo(puzzleInput1, 3);
            List<FlightInstruction> puzzleInput2 = GetPuzzleDataTwo();
            //get arr file
            Debug.WriteLine("The answer you seek is " + AdventCalenderPuzzleFunctionOne(puzzleInput1) + ".");
            Debug.WriteLine("The answer to puzzle two is " + AdventCalenderPuzzleFunctionOne(sumList) + ".");
            Debug.WriteLine("This is a tricky one. But for puzzle three, the answer be: " + AdventCalenderPuzzleFunctionThree(puzzleInput2));
            Debug.WriteLine("Well, turns out that last answer was rubbish. We needed more data! I now calculate our current position to be: " + AdventCalenderPuzzleFunctionFour(puzzleInput2));
        }

        class FlightInstruction
        {
            public string direction { get; set; }
            public int value { get; set; }
        }
        static List<int> GetPuzzleDataOne()
        {
            List<int> numberlist = new List<int>();
            string line;
            StreamReader reader = File.OpenText("AdventPuzzleInput1.txt");

            while ((line = reader.ReadLine()) != null)
            {
                numberlist.Add(Int32.Parse(line));
            }
            return numberlist;
        }
        static List<FlightInstruction> GetPuzzleDataTwo()
        {
            List<FlightInstruction> flightInstructions = new List<FlightInstruction>();
            string line;
            StreamReader reader = File.OpenText("AdventPuzzleInput2.txt");
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

        static int AdventCalenderPuzzleFunctionThree(List<FlightInstruction> flightInstructions)
        {
            int horizontalPos = 0, depth = 0;

            foreach (var instruction in flightInstructions)
            {
                switch (instruction.direction)
                {
                    case "forward":
                        horizontalPos += instruction.value;
                        break;
                    case "down":
                        depth += instruction.value;
                        break;
                    case "up":
                        depth -= instruction.value;
                        break;
                    default:
                        break;
                }
            }
            return horizontalPos * depth;
        }

        static int AdventCalenderPuzzleFunctionFour(List<FlightInstruction> flightInstructions)
        {
            int horizontalPos = 0, depth = 0, aim = 0;

            foreach (var instruction in flightInstructions)
            {
                switch (instruction.direction)
                {
                    case "forward":
                        horizontalPos += instruction.value;
                        depth += aim * instruction.value;
                        break;
                    case "down":
                        aim += instruction.value;
                        break;
                    case "up":
                        aim -= instruction.value;
                        break;
                    default:
                        break;
                }
            }
            return horizontalPos * depth;
        }
        static int AdventCalenderPuzzleFunctionOne(List<int> list)
        {
            int increaseCount = 0;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] > list[i - 1]) { increaseCount++; }
            }
            return increaseCount;
        }
        static List<int> AdventCalenderPuzzleFunctionTwo(List<int> list, int divider)
        {
            List<int> newList = new List<int>();

            for (int i = divider; i <= list.Count; i++)
            {
                int newInt = 0;
                for (int x = i - 1; x >= i - divider; x--)
                {
                    newInt += list[x];
                }
                newList.Add(newInt);
            }
            return newList;
        }

    }
}
