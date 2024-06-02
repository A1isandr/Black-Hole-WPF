using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Black_Hole.Helpers
{
    public static class Vector2Extension
    {
        public static float Magnitude(this Vector2 vector)
        {
            return (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static float MagnitudeSqr(this Vector2 vector)
        {
            return vector.X * vector.X + vector.Y * vector.Y;
        }

        public static Vector2 SetMagnitude(this Vector2 vector, float desiredMagnitude)
        {
            vector.X = vector.X * desiredMagnitude / vector.Magnitude();
            vector.Y = vector.Y * desiredMagnitude / vector.Magnitude();

            //MessageBox.Show(vector.ToString());

            return vector;
        }

        //public static void Normalize(this Vector2 vector)
        //{
        //    var len = vector.Magnitude();

        //    // Здесь мы умножаем на обратное значение вместо вызова 'div()'
        //    // поскольку div дублирует эту проверку на ноль.
        //    if (len != 0) vector *= (1 / len);
        //}

        public static Vector2 Limit(this Vector2 vector, float limit)
        {
            float mSq = vector.MagnitudeSqr();

            if (mSq > limit * limit)
            {
                vector /= vector.Magnitude(); // нормализуем вектор;
                vector *= limit;
            }

            return vector;
        }
    }
}
