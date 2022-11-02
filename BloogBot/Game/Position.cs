using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace BloogBot.Game
{
    [StructLayout(LayoutKind.Sequential)]
    public class Position
    {
        public Position(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Position(float x, float y, float z, int id)
        {
            X = x;
            Y = y;
            Z = z;
            Id = id;
        }
        public float X { get; }

        public float Y { get; }

        public float Z { get; }

        public float Id { get; }

        public override string ToString() => $"X: {Math.Round(X, 2)}, Y: {Math.Round(Y, 2)}, Z: {Math.Round(Z, 2)}";

        public float DistanceTo(Position position)
        {
            var deltaX = X - position.X;
            var deltaY = Y - position.Y;
            var deltaZ = Z - position.Z;

            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }

        public float DistanceTo2D(Position position)
        {
            var deltaX = X - position.X;
            var deltaY = Y - position.Y;

            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
