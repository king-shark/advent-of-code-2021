using System;
using System.Collections.Generic;
using System.IO;
namespace AdventCalendar
{
    public class NavigationSystems
    {
        public List<FlightInstruction> flightInstructions = new List<FlightInstruction>();
        public NavigationSystems(string filename)
        {
            string line;
            StreamReader reader = File.OpenText(filename);
            while ((line = reader.ReadLine()) != null)
            {
                string[] lineArray = line.Split(' ');
                FlightInstruction instruction = new FlightInstruction
                {
                    direction = lineArray[0],
                    value = Int32.Parse(lineArray[1])
                };
                flightInstructions.Add(instruction);
            }
        }
        public class FlightInstruction
        {
            public string direction { get; set; }
            public int value { get; set; }
        }
        public int CalculateFlightPath(bool aiming)
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
    }
}
