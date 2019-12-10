using KillAllNeighbors.Resources.Builder;
using KillAllNeighbors.Resources.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KillAllNeighbors.Resources.ChainOfResponsability
{
    abstract class HandleGameOver
    {
        protected HandleGameOver Successor;

        public void SetSuccessor(HandleGameOver supervisor)
        {
            this.Successor = supervisor;
        }

        public abstract void EndGame(Facade.Facade facade,Enemy enemy,Form1 board,Player player);
    }
}
