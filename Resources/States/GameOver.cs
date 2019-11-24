using KillAllNeighbors.Resources.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.State
{
    public class GameOver : GameState
    {
        public override async void Handle(ConnectionHandler context)
        {
            await context.DisConnect();
        }
    }
}
