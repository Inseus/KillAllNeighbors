using KillAllNeighbors.Resources.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Visitor
{
    class DrawingElements
    {
        private List<DrawingElement> _elements = new List<DrawingElement>();
        public void Attach(DrawingElement employee)
        {
            _elements.Add(employee);
        }
        public void Detach(DrawingElement employee)
        {
            _elements.Remove(employee);
        }
        public void Accept(IVisitors visitor)
        {
            foreach (DrawingElement e in _elements)
            {
                e.Accept(visitor);
            }
            Console.WriteLine();
        }
    }
}
