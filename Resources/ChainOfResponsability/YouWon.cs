using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KillAllNeighbors.Resources.Decorator;
using KillAllNeighbors.Resources.Facade;
using KillAllNeighbors.Resources.State;

namespace KillAllNeighbors.Resources.ChainOfResponsability
{
    class YouWon : HandleGameOver
    {
        public override void EndGame(Facade.Facade facade, Enemy enemy, Form1 board, Player player)
        {
            if (player.id == enemy.whoWon)
            {
                board.LabelText = "You won";
                facade.stopTimers();
                facade.setState(new GameOver());
                facade.gameAction();
            }
            else
            {
                Successor.EndGame(facade, enemy, board, player);
            }
        }
    }
}
