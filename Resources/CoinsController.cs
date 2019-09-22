using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources
{
    public class CoinsController
    {
        private int maxCoins = 10;
        private int currentCoins = 0;
        Random seed;

        List<Coin> coinList = new List<Coin>();

        public CoinsController()
        {
            seed = new Random();
        }

        public Coin SpawnNewCoin()
        {
            if(currentCoins <= maxCoins)
            {
                Coin _tempCoin = new Coin(1, seed);
                currentCoins++;
                coinList.Add(_tempCoin);
                return _tempCoin;
            }
            return null;
        }

        public List<Coin> GetCoinList()
        {
            return coinList;
        }

        public void RemoveCoin(Coin coin)
        {
            coinList.Remove(coin);
            currentCoins--;
        }
    }
}
