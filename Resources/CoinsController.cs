using KillAllNeighbors.Resources.Iterator;
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
        private CoinsCollection coinList = new CoinsCollection();
        private CoinsIterator coinsIterator;

        public CoinsController()
        {
            coinsIterator = new CoinsIterator(coinList);
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

        public ICurrency GetCollidingCoin(PictureBox moveableObj)
        {
            if (CoinsHandler.Instance.IsIntersecting(coinsIterator.First(), moveableObj))
                return coinsIterator.CurrentCoin;

            while (!coinsIterator.IsEnd)
            {
                var _tmpCoin = coinsIterator.Next();
                if (_tmpCoin != null)
                    if (CoinsHandler.Instance.IsIntersecting(coinsIterator.CurrentCoin, moveableObj))
                    {
                        CoinsHandler.Instance.AddCoins(coinsIterator.CurrentCoin.Value);
                        return coinsIterator.CurrentCoin;
                    }
            }

            return null;
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
            coinsIterator.Add(_tempCoin);
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
