using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KillAllNeighbors.Resources.Decorator;
using KillAllNeighbors.Resources.Facade;

namespace KillAllNeighbors.Resources.ChainOfResponsability
{
    class YouLost : HandleGameOver
    {     
        public override void EndGame(Facade.Facade facade, Enemy enemy, Form1 board, Player player)
        {
            board.LabelText = "Game over player" + enemy.whoWon + " won";
            facade.stopTimers();
        }
    }
}
