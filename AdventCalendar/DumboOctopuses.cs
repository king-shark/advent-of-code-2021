using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace AdventCalendar
{
    public class DumboOctopuses
    {
        int[,] octopi = new int[10, 10];
        List<Coord> flashers = new List<Coord>();
        public long tempCount = 0;
        public long FlashCount = 0;
        public long StepCount = 0;

        public DumboOctopuses(string filename, int stepCount = 0)
        {
            string line;
            StreamReader reader = File.OpenText(filename);
            int i = 0;
            int j = 0;
            while ((line = reader.ReadLine()) != null)
            {
                j = 0;
                foreach (var c in line.ToArray())
                {
                    octopi[i, j] = Int32.Parse(c.ToString());
                    j++;
                }
                i++;
            }
            DoSteps(stepCount);
            FlashCount = tempCount;
            while (flashers.Count != 100)
            {
                ContinueSteps();
            }
        }
        public void ContinueSteps()
        {
            DoSteps(1);
        }
        public void DoSteps(int stepCount)
        {
            for (int sc = 0; sc < stepCount; sc++)
            {
                StepCount++;
                Step();
            }
        }
        public void Step()
        {
            IncrementValues();
            DoFlash();
            Reset();
        }
        public void IncrementValues()
        {
            flashers = new List<Coord>();
            for (int i = 0; i < octopi.GetLength(0); i++)
            {
                for (int j = 0; j < octopi.GetLength(1); j++)
                {
                    octopi[i, j]++;
                    if (octopi[i, j] > 9) { AddFlasher(new Coord(i, j)); }
                }
            }
        }
        public void DoFlash()
        {
            for (int c = 0; c < flashers.Count; c++)
            {
                tempCount++;
                UpdateNeighbors(flashers[c]);
            }
        }
        public void Reset()
        {
            foreach (Coord coord in flashers)
            {
                octopi[coord.x, coord.y] = 0;
            }
        }
        public void AddFlasher(Coord coord)
        {
            if (!flashers.Exists(c => c.x == coord.x && c.y == coord.y)) { flashers.Add(coord); }
        }
        public void UpdateNeighbors(Coord coord)
        {
            foreach (Coord neighbor in coord.Neighbors)
            {
                if (neighbor.x >= 0 && neighbor.x < octopi.GetLength(0) && neighbor.y >= 0 && neighbor.y < octopi.GetLength(1))
                {
                    octopi[neighbor.x, neighbor.y]++;
                    if (octopi[neighbor.x, neighbor.y] > 9) { AddFlasher(neighbor); }
                }
            }
        }
    }

    public class Coord
    {
        private string val = "";
        public List<Coord> Neighbors
        {
            get
            {
                List<Coord> newList = new List<Coord>();
                newList.Add(new Coord(x - 1, y - 1));
                newList.Add(new Coord(x - 1, y));
                newList.Add(new Coord(x - 1, y + 1));
                newList.Add(new Coord(x, y - 1));
                newList.Add(new Coord(x, y + 1));
                newList.Add(new Coord(x + 1, y - 1));
                newList.Add(new Coord(x + 1, y));
                newList.Add(new Coord(x + 1, y + 1));
                return newList;
            }
        }
        public int x
        {
            get
            {
                return Int32.Parse(val.Split(',')[0]);
            }
        }
        public int y
        {
            get
            {
                return Int32.Parse(val.Split(',')[1]);
            }
        }
        public Coord(int x, int y)
        {
            val = x + "," + y;
        }
    }
}
