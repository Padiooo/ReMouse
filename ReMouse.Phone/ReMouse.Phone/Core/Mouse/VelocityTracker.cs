using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ReMouse.Phone.Core.Mouse
{
    public class VelocityTracker
    {
        private readonly Stopwatch stopwatch = new Stopwatch();

        private readonly Queue<Point> points = new Queue<Point>();
        private Point lastPoint = Point.Zero;

        private Point VelocityPoint;
        public float XVelocity => VelocityPoint.X / VelocityPoint.Elapsed;
        public float YVelocity => VelocityPoint.Y / VelocityPoint.Elapsed;

        public VelocityTracker()
        {
            Reset();
        }

        public void AddPoint(float x, float y)
        {
            Point p = new Point(x, y, stopwatch.ElapsedMilliseconds);

            if (lastPoint == Point.Zero)
            {
                lastPoint = p;
                return;
            }

            Point delta = p - lastPoint;
            lastPoint = p;
            points.Enqueue(delta);
            if (points.Count > 5)
                VelocityPoint -= points.Dequeue();

            VelocityPoint += delta;
        }

        public void Reset()
        {
            stopwatch.Restart();
            points.Clear();
            VelocityPoint = Point.Zero;
        }

        private struct Point : IEquatable<Point>
        {
            public static readonly Point Zero = new Point(0, 0, 0);

            public float X;
            public float Y;
            public long Elapsed;

            public Point(float x, float y, long elapsed)
            {
                X = x;
                Y = y;
                Elapsed = elapsed;
            }

            public override bool Equals(object obj)
            {
                return obj is Point point && Equals(point);
            }

            public bool Equals(Point other)
            {
                return Y == other.Y &&
                       X == other.X;
            }

            public override int GetHashCode()
            {
                int hashCode = -1586994682;
                hashCode = hashCode * -1521134295 + X.GetHashCode();
                hashCode = hashCode * -1521134295 + Y.GetHashCode();
                hashCode = hashCode * -1521134295 + Elapsed.GetHashCode();
                return hashCode;
            }

            public static Point operator +(Point p1, Point p2) => new Point(p1.X + p2.X, p1.Y + p2.Y, p1.Elapsed + p2.Elapsed);
            public static Point operator -(Point p1, Point p2) => new Point(p1.X - p2.X, p1.Y - p2.Y, p1.Elapsed - p2.Elapsed);
            public static bool operator ==(Point p1, Point p2) => p1.Equals(p2);
            public static bool operator !=(Point p1, Point p2) => !p1.Equals(p2);
        }
    }
}
