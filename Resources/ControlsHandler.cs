using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace KillAllNeighbors
{
    interface IControls
    {
        Vector2 RightUpwards();
        Vector2 LeftUpwards();
        Vector2 RightDownwards();
        Vector2 LeftDownwards();
        Vector2 Upwards();
        Vector2 Downwards();
        Vector2 Right();
        Vector2 Left();
    }

    public class ControlsHandler : Form1, IControls
    {
        private static ControlsHandler instance = null;
        private static readonly object instanceLock = new object();
        private int speedMultiplier = 5;
        //private int _keysVarNumbers = { (int)Key.W, (int)Key.A }; Continue here
        public static ControlsHandler Instance {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new ControlsHandler();
                    return instance;
                }
            }
        }

        private Vector2 zero;
        public ControlsHandler()
        {
            zero = new Vector2(0, 0);
        }

        public Vector2 GetVector()
        {
            Vector2 _temp = new Vector2();
            foreach (Vector2 vector in GetCoord())
            {
                _temp += vector * speedMultiplier;
            }
            return _temp;
        }

        private IEnumerable GetCoord()
        {
            yield return RightUpwards();
            yield return LeftUpwards();
            yield return RightDownwards();
            yield return LeftDownwards();
            yield return Upwards();
            yield return Downwards();
            yield return Right();
            yield return Left();
        }

        public Vector2 RightUpwards()
        {
            return Keyboard.IsKeyDown(Key.D) && Keyboard.IsKeyDown(Key.W) ? new Vector2(1, -1) : zero;
        }
        public Vector2 LeftUpwards()
        {
            return Keyboard.IsKeyDown(Key.A) && Keyboard.IsKeyDown(Key.W) ? new Vector2(-1, -1) : zero;
        }

        public Vector2 RightDownwards()
        {
            return Keyboard.IsKeyDown(Key.D) && Keyboard.IsKeyDown(Key.S) ? new Vector2(1, 1) : zero;
        }

        public Vector2 LeftDownwards()
        {
            return Keyboard.IsKeyDown(Key.A) && Keyboard.IsKeyDown(Key.S) ? new Vector2(-1, 1) : zero;
        }

        public Vector2 Upwards()
        {
            return Keyboard.IsKeyDown(Key.W) ? new Vector2(0, -1) : zero;
        }

        public Vector2 Downwards()
        {
            return Keyboard.IsKeyDown(Key.S) ? new Vector2(0, 1) : zero;
        }

        public Vector2 Right()
        {
            return Keyboard.IsKeyDown(Key.D)? new Vector2(1, 0) : zero;
        }

        public Vector2 Left()
        {
            return Keyboard.IsKeyDown(Key.A)? new Vector2(-1, 0) : zero;
        }

        public bool IsAnyKeyDown()
        {
            if (!(Keyboard.IsKeyDown(Key.W) || Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.A) || Keyboard.IsKeyDown(Key.D)))
            {
                return false;
            }
            return true;
        }

        public int KeysDownCount()
        {
            int _count = 0;
            int[] _keysVarNumbers = { 66, 44, 62, 47 };
            for (int i = 0; i < _keysVarNumbers.Length; i++)
            {

            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                _count++;
            }
            
        }
    }
}
