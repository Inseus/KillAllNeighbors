﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Strategy.Implementation
{
    class Pistol : ShootAlgorithm
    {
        public override Bullet Shoot()
        {
            return new Bullet(60);
        }
    }
}
