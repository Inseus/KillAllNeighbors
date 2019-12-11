using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Command.Memento
{
    public class Memento
    {
        int coinsCount;
        public  Memento(int coinsCount)
        {
            this.coinsCount = coinsCount;
        }
        public void getCoinsCount(CoinsHandler originator)
        {
            originator.setCoinsCount(coinsCount);
        }
    }
}
