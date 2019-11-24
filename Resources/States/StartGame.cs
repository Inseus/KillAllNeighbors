using KillAllNeighbors.Resources.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.States
{
    public class StartGame : GameState
    {
        public override async void Handle(ConnectionHandler context)
        {
            await context.Connect();
        }
    }
}
