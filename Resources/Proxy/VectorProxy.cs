using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KillAllNeighbors.Proxy
{
    class VectorProxy : Vector2
    {
        Vector2 _vector = new Vector2();
        public Vector2 Zero()
        {
            return new Vector2(0, 0);
        }
        public Vector2 Up()
        {
            return _vector.Up();
        }
        public Vector2 Down()
        {
            return _vector.Down();
        }
        public Vector2 Right()
        {
            return _vector.Right();
        }
        public Vector2 Left()
        {
            return _vector.Left();
        }
    }
}
