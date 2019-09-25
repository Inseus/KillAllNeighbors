using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KillAllNeighbors.Resources
{
    /// <summary>
    /// CoinsHandler handles existing coin data:
    /// Adds to player coins
    /// Removes from player coins
    /// </summary>
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

        public int GetCoinsCount()
        {
            return coinsCount;
        }
        public void AddCoins(int number = 1)
        {
            coinsCount += number;
        }

        public Coin TryCollectCoin(PictureBox moveableObject, List<Coin> coinList)
        {
            for (int i = 0; i < coinList.Count; i++)
            {
                if (IsIntersecting(coinList[i], moveableObject))
                {
                    coinsCount += coinList[i].GetValue();
                    return coinList[i];
                }
            }
            return null;
        }

        private bool IsIntersecting(Coin coin, PictureBox moveableObject)
        {
            if (coin.GetFormControlItem().Bounds.IntersectsWith(moveableObject.Bounds))
            {
                return true;
            }
            return false;
        }
    }
}
