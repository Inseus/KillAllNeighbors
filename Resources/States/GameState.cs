using KillAllNeighbors.Resources.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.State
{
    public abstract class GameState
    {
        public abstract void Handle(ConnectionHandler context);
    }
}
