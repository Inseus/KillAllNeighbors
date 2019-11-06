using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Command
{
    class ConcreteCommand : Command
    {
        private Player thisPlayer;
        private int x, y;
        private Receiver receiver;
        public ConcreteCommand(Receiver receiver,int x, int y, Player thisPlayer)
        {
            this.receiver = receiver;
            this.thisPlayer = thisPlayer;
            this.x = x;
            this.y = y;
        }
        public override void Execute()
        {
            receiver.Do(x,y,thisPlayer);
        }

        public override void UnExecute()
        {
            receiver.Undo(x, y, thisPlayer);
            
        }
    }
}
