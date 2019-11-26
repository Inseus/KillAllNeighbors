using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Iterator
{
    interface  ICoinsCollection
    {
        ICoinsIterator CreateIterator();
    }
    class CoinsCollection : ICoinsCollection
    {
        private ArrayList coinList = new ArrayList();

        public ICoinsIterator CreateIterator()
        {
            return new CoinsIterator(this);
        }

        public int Count
        {
            get { return coinList.Count; }
        }

        public void Remove(ICurrency coin)
        {
            this.coinList.Remove(coin);
        }

        public void RemoveAt(int index)
        {
            this.coinList.RemoveAt(index);
        }

        public ICurrency this[int index]
        {
            get
            {
                if (coinList.Count == 0)
                    return null;
                return coinList[index] as ICurrency;
            }
            set { coinList.Add(value); }
        }
    }
}
