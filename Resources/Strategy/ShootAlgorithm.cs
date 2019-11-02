using KillAllNeighbors.Resources.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Strategy
{
    public abstract class ShootAlgorithm
    {
        protected CreatorOfPictureBox creator;
        public ShootAlgorithm(CreatorOfPictureBox _creator)
        {
            creator = _creator;
        }
        public abstract Bullet Shoot();
    }
}
