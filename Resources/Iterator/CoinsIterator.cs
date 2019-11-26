using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Iterator
{
    interface ICoinsIterator
    {
        ICurrency First();
        ICurrency Next();
        ICurrency CurrentCoin { get; }
        bool IsEnd { get; }
    }

    class CoinsIterator : ICoinsIterator
    {
        private CoinsCollection coins;
        private int current = 0;
        private int step = 1;

        public CoinsIterator(CoinsCollection coins)
        {
            this.coins = coins;
        }

        public ICurrency First()
        {
            current = 0;
            return coins[current] as ICurrency;
        }

        public ICurrency Next()
        {
            current += step;
            if (!IsEnd)
                return coins[current] as ICurrency;
            else
                return null;
        }

        public ICurrency CurrentCoin
        {
            get { return coins[current] as ICurrency; }
        }

        public bool IsEnd
        {
            get { return current >= coins.Count; }
        }

        public bool Contains(ICurrency coin)
        {
            if (coins.Count == 0)
                return false;
            if (First().Equals(coin))
                return true;
            while (!IsEnd)
            {
                ICurrency _tempCur = Next();
                if(_tempCur != null)
                    if(_tempCur.Equals(coin))
                        return true;
            }
            return false;
        }

        public void Add(ICurrency coin)
        {
            coins[coins.Count] = coin;
        }
    }
}
