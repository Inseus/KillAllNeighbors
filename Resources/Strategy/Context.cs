using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Strategy
{
    class Context
    {
        private ShootAlgorithm _strategy;

        // Constructor

        public Context(ShootAlgorithm strategy)
        {
            this._strategy = strategy;
        }

        public Bullet ContextInterface()
        {
            return _strategy.Shoot();
        }
    }
}
