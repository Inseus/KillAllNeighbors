using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Strategy.Implementation
{
    class Pistol : ShootAlgorithm
    {
        public bool Shoot()
        {
            return Keyboard.IsKeyDown(Key.NumPad1) ? true : false;
        }
    }
}
