using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.IO;

namespace AdventCalendar
{
    public class GeoThermalVentMap
    {
        int[,] map;

        public GeoThermalVentMap(string filename, int width, int height, bool includeDiagonal)
        {
            map = new int[width, height];
            StreamReader reader = File.OpenText(filename);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] lineArray = line.Split(" -> ");
                string[] point0 = lineArray[0].Split(',');
                string[] point1 = lineArray[1].Split(',');
                Vent vent = new Vent(new Point(Int32.Parse(point0[0]), Int32.Parse(point0[1])), new System.Drawing.Point(Int32.Parse(point1[0]), Int32.Parse(point1[1])));
                if (vent.IsHorizontal || vent.IsVertical)
                {
                    AddVent(vent);
                }
                else if (includeDiagonal)
                {
                    AddDiagonalVent(vent);
                }
            }
        }
        public void AddVent(Vent vent)
        {
            for (int x = Math.Min(vent.point1.X, vent.point2.X); x <= Math.Max(vent.point2.X, vent.point1.X); x++)
            {
                for (int y = Math.Min(vent.point1.Y,vent.point2.Y); y <= Math.Max(vent.point1.Y, vent.point2.Y); y++)
                {
                    map[x, y]++;
                }
            }
        }
        public void AddDiagonalVent(Vent vent)
        {
            int slopeX;
            int slopeY;
            slopeX = -(vent.point1.X - vent.point2.X);
            slopeX = slopeX > 0 ? 1 : -1;
            slopeY = -(vent.point1.Y - vent.point2.Y);
            slopeY = slopeY > 0 ? 1 : -1;
            for (int i = 0; i <= Math.Abs(vent.point1.X - vent.point2.X); i++)
            {
                map[vent.point1.X + i * slopeX, vent.point1.Y + i * slopeY]++;
            }
        }
        public int GetVentDensity(int density)
        {
            int count = 0;
            for (int x = 0; x <= map.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= map.GetUpperBound(1); y++)
                {
                    if (map[x, y] >= density) { count++; }
                }
            }
            return count;
        }
    }
    public class Vent
    {
        public Point point1 { get; set; }
        public Point point2 { get; set; }
        public bool IsHorizontal
        {
            get
            {
                return point1.Y == point2.Y;
            }
        }
        public bool IsVertical
        {
            get
            {
                return point1.X == point2.X;
            }
        }

        public Vent(Point point1, Point point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }
    }
}
