using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors
{
    public class Vector2
    {
        public int x;
        public int y;

        public Vector2()
        {
            this.x = 0;
            this.y = 0;
        }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 Zero()
        {
            return new Vector2(0, 0);
        }

        public Vector2 One()
        {
            return new Vector2(1,1);
        }

        public static Vector2 operator+ (Vector2 a, Vector2 b)
        {
            Vector2 _temp = new Vector2();
            _temp.x += a.x + b.x;
            _temp.y += a.y + b.y;
            return _temp;
        }

        public static Vector2 operator* (Vector2 a, int b)
        {
            Vector2 _temp = new Vector2();
            _temp.x += a.x * b;
            _temp.y += a.y * b;
            return _temp;
        }
    }
}
