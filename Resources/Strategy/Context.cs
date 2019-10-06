using KillAllNeighbors.Resources.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Strategy
{
    public class Context
    {
        private ShootAlgorithm _strategy;

        // Constructor

        public Context(ShootAlgorithm strategy)
        {
            this._strategy = strategy;
        }

        public Bullet ContextInterface(CreatorOfPictureBox creator)
        {
            return _strategy.Shoot(creator);
        }
    }
}
