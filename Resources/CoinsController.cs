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
        private int maxCoins = 50;
        private int currentCoins = 0;
        private Random seed;
        private Timer coinSpawnTimer = new Timer { Interval = 1000 }; //Every 1 sec
        private List<Coin> coinList = new List<Coin>();

        public CoinsController()
        {
            seed = new Random();
        }

        public Coin SpawnNewCoin()
        {
            if(currentCoins <= maxCoins)
            {
                return AddRandomCoin();
            }
            else
            {
                return RemoveRandomCoin();
            }
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

        private Coin AddRandomCoin()
        {
            Coin _tempCoin = new Coin(1, seed);
            currentCoins++;
            coinList.Add(_tempCoin);
            return _tempCoin;
        }

        private Coin RemoveRandomCoin()
        {
            Random _randomCoinSeed = new Random();
            int _takeIndex = _randomCoinSeed.Next(coinList.Count);
            Coin _tempCoin = coinList[_takeIndex];
            coinList.RemoveAt(_takeIndex);
            currentCoins--;
            return _tempCoin;
        }
    }
}
