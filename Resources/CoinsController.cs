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
        private List<ICurrency> coinList = new List<ICurrency>();

        public CoinsController()
        {
            seed = new Random();
        }

        public ICurrency SpawnNewCoin()
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

        public List<ICurrency> GetCoinList()
        {
            return coinList;
        }

        public void RemoveCoin(ICurrency coin)
        {
            coinList.Remove(coin);
            currentCoins--;
        }

        private ICurrency AddRandomCoin()
        {
            ICurrency _tempCoin = CoinsFactory.MakeCoin(seed.Next(1, 11), seed.Next(Constants.MIN_BOUND_X, Constants.VIEW_SIZE_X), seed.Next(Constants.MIN_BOUND_Y, Constants.VIEW_SIZE_Y));
            currentCoins++;
            coinList.Add(_tempCoin);
            return _tempCoin;
        }

        private ICurrency RemoveRandomCoin()
        {
            Random _randomCoinSeed = new Random();
            int _takeIndex = _randomCoinSeed.Next(coinList.Count);
            ICurrency _tempCoin = coinList[_takeIndex];
            coinList.RemoveAt(_takeIndex);
            currentCoins--;
            return _tempCoin;
        }
    }
}
