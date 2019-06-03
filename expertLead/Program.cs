/*
|
|
|
| + (1,4)   + (4,4)
|
|     
|       + (3,2)
|                       + (5,1)
|________________________

Given a set of coordinates [(x1,y2), ..., (xn,yn)], 
create a testable application that determines:
 - the two closest points
 - the two most distant points
Use your knowledge of DDD, OOP and Clean Code.
Start with tests or implementation, whatever is better for you.
*/

// sqrt((x1 - x2)^2 + (y1 - y2)^2)

using System;
using System.Collections.Generic;
using NUnit.Framework;
// To execute C#, please define "static void Main" on a class
// named Solution.

namespace expertLead
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Random rnd = new Random();
            Calculation calc = new Calculation();
            List<Coordinates> coord = new List<Coordinates>();
            Coordinates testCoord;

            for (int i = 1; i <= 10; i++)
            {
                testCoord = new Coordinates
                {
                    X = rnd.Next(0, 99),
                    Y = rnd.Next(0, 99)
                };

                coord.Add(testCoord);

                Console.WriteLine("Coordinate " + i.ToString() + ": (" + testCoord.X.ToString() + "," + testCoord.Y.ToString() + ")");
            }

            Distance close = calc.PointDistance(coord, true);
            Console.WriteLine("Closest Points: " + close.DistanceValue.ToString());
            Console.WriteLine("Point A:");
            Console.WriteLine("X: " + close.PointA.X.ToString());
            Console.WriteLine("Y: " + close.PointA.Y.ToString());
            Console.WriteLine("Point B:");
            Console.WriteLine("X: " + close.PointB.X.ToString());
            Console.WriteLine("Y: " + close.PointB.Y.ToString());

            Distance far = calc.PointDistance(coord, false);
            Console.WriteLine("Most Distant Points: " + far.DistanceValue.ToString());
            Console.WriteLine("Point A:");
            Console.WriteLine("X: " + far.PointA.X.ToString());
            Console.WriteLine("Y: " + far.PointA.Y.ToString());
            Console.WriteLine("Point B:");
            Console.WriteLine("X: " + far.PointB.X.ToString());
            Console.WriteLine("Y: " + far.PointB.Y.ToString());
        }
    }

    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Distance
    {
        public Coordinates PointA { get; set; }
        public Coordinates PointB { get; set; }
        public double DistanceValue { get; set; }
    }

    public class Calculation
    {
        public Distance PointDistance(List<Coordinates> coord, bool closest)
        {
            Distance cacheDis;
            List<Distance> dist = new List<Distance>();

            foreach (Coordinates c in coord)
            {
                foreach (Coordinates o in coord)
                {
                    cacheDis = new Distance
                    {
                        DistanceValue = Math.Sqrt((c.X - o.X) ^ 2 + (c.Y - o.Y) ^ 2)
                    };

                    if (cacheDis.DistanceValue > 0.0)
                    {
                        cacheDis.PointA = c;
                        cacheDis.PointB = o;

                        dist.Add(cacheDis);
                    }
                }
            }

            dist.Sort(delegate (Distance a, Distance b)
            {
                return a.DistanceValue.CompareTo(b.DistanceValue);
            });

            if (closest)
                return dist[0];

            return dist[dist.Count - 1];
        }
    }

    [TestFixture]
    public class UnitTest
    {
        Calculation calc = new Calculation();
        List<Coordinates> coord = new List<Coordinates>();
        Coordinates testCoord = new Coordinates();

        [Test]
        public void ShortTest()
        {
            double expected = 1.0;

            testCoord.X = 1;
            testCoord.Y = 1;
            coord.Add(testCoord);

            testCoord.X = 9;
            testCoord.Y = 9;
            coord.Add(testCoord);

            testCoord.X = 2;
            testCoord.Y = 2;
            coord.Add(testCoord);

            testCoord.X = 5;
            testCoord.Y = 5;
            coord.Add(testCoord);

            testCoord.X = 7;
            testCoord.Y = 7;
            coord.Add(testCoord);

            Assert.AreEqual(expected, calc.PointDistance(coord, true));
        }

        [Test]
        public void LongTest()
        {
            double expected = 1.0;

            testCoord.X = 1;
            testCoord.Y = 1;
            coord.Add(testCoord);

            testCoord.X = 9;
            testCoord.Y = 9;
            coord.Add(testCoord);

            testCoord.X = 2;
            testCoord.Y = 2;
            coord.Add(testCoord);

            testCoord.X = 5;
            testCoord.Y = 5;
            coord.Add(testCoord);

            testCoord.X = 7;
            testCoord.Y = 7;
            coord.Add(testCoord);

            Assert.AreEqual(expected, calc.PointDistance(coord, false));
        }
    }
}