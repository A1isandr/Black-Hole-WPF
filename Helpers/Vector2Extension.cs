using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Black_Hole.Helpers
{
    public static class Vector2Extension
    {
        public static float Magnitude(this Vector2 vector)
        {
            return (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector2 SetMagnitude(this Vector2 vector, float magnitude)
        {

        }

        public static Vector2 Normalize(this Vector2 vector)
        {

        }
    }
}
