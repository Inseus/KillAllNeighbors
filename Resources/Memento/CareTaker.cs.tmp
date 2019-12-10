using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Command.Memento
{
    class CareTaker
    {
        static List<Memento> savedHistory;
        public CareTaker()
        {
            savedHistory = new List<Memento>();
        }
        public void addMemento(Memento m)
        {
            savedHistory.Add(m);
        }
        public Memento getMemento()
        {
            Memento last;
            if (savedHistory.Count>0)
            {
                last = savedHistory.Last();
                savedHistory.RemoveAt(savedHistory.Count - 1);
            }
            else
            {
                last = new Memento(0);
            }

            return last;
        }
    }
}
