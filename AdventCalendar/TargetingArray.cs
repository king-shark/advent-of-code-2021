using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventCalendar
{
    public class TargetingArray
    {
        Coord Projectile = new Coord(0, 0);
        public int VelocityCount;
        int VelocityX;
        int VelocityY;
        Target Target;
        public TargetingArray(Target target)
        {
            Target = target;
            
        }
        public int CalculateTrickShot()
        {
            int temp = 0 - Target.max.y;
            return temp * (temp - 1) / 2;
        }
        public int CalculateValidTrajectories()
        {
            VelocityCount = 0;
            List<int> yrange = new List<int>(Enumerable.Range(Target.max.y, Math.Abs(Target.max.y * 2)));
            List<int> xrange = new List<int>(Enumerable.Range(Target.min.x, Target.max.x - Target.min.x + 1));
            xrange.AddRange(Enumerable.Range(1, (int)Math.Ceiling((decimal)Target.max.x/2)));
            foreach (var x in xrange)
            {
                foreach (var y in yrange)
                {
                    Projectile = new Coord(0, 0);
                    TestVelocity(x, y);
                }
            }
            return VelocityCount;
        }
        public void TestVelocity(int velX, int velY)
        {
            VelocityX = velX;
            VelocityY = velY;
            string str = Step();
            if (str == "Hit") { VelocityCount++; }
            if (str == "Error") { Debug.WriteLine("Something went wrong x:{0}, y:{1}", velX, velY); }
        }

        public string Step()
        {
            do
            {
                Projectile.x += VelocityX;
                Projectile.y += VelocityY;
                if (VelocityX > 0) VelocityX--;
                if (VelocityX < 0) VelocityX++;
                VelocityY--;
                if (Target.InTarget(Projectile)) return "Hit";
                if (Target.MissedTarget(Projectile)) return "Miss";
            } while (!Target.InTarget(Projectile) || !Target.MissedTarget(Projectile));
            return "Error";
        }
    }

    public class Target
    {
        public Coord min;
        public Coord max;
        public Target(int xmin, int xmax, int ymin, int ymax)
        {
            min = new Coord(xmin, ymin);
            max = new Coord(xmax, ymax);
        }
        public bool InTarget(Coord coord)
        {
            if (coord.x >= min.x && coord.y <= min.y && coord.x <= max.x && coord.y >= max.y) return true;
            else return false;
        }
        public bool MissedTarget(Coord coord)
        {
            if (coord.x > max.x || coord.y < max.y) return true;
            else return false;
        }
    }
}