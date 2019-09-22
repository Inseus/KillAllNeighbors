using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KillAllNeighbors.Resources
{
    public class CoinsHandler
    {
        private int coinsCount = 0;

        private static CoinsHandler instance = null;
        private static readonly object instanceLock = new object();

        public static CoinsHandler Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                        instance = new CoinsHandler();
                    return instance;
                }
            }
        }

        public void AddCoins(int number = 1)
        {
            coinsCount += number;
        }

        public void TryCollectCoin(PictureBox moveableObject)
        {
            for (int i = 0; i < 0 /* CoinsController coinList */; i++)
            {
                if(IsIntersecting(/*coinList[i]*/, moveableObject))
                {
                    coinsCount += coinList[i].GetValue();
                }
            }
        }

        private bool IsIntersecting(Coin coin, PictureBox moveableObject)
        {
            if (coin.GetPicture().Bounds.IntersectsWith(moveableObject.Bounds))
            {
                return true;
            }
            return false;
        }
    }
}
