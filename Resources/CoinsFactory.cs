using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources
{
    public static class CoinsFactory
    {
        public static ICurrency MakeCoin(int coinPercentageIndentifier, int posX, int posY)
        {
            if (coinPercentageIndentifier < 40)
            {
                return new DefaultCoin(posX, posY);
            }
            else if (coinPercentageIndentifier > 40 && coinPercentageIndentifier < 70)
            {
                return new CoolCoin(posX, posY);
            }
            else if (coinPercentageIndentifier > 70 && coinPercentageIndentifier < 90)
            {
                return new SuperCoin(posX, posY);
            }
            else
            {
                return new MegaCoin(posX, posY);
            }
        }
    }
}
