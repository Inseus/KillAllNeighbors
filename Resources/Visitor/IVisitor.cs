using KillAllNeighbors.Resources.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Visitor
{
    public interface IVisitors
    {
        void VisitCompositeElement(CompositeElement concreteElementA);

        void VisitPrimitiveElement(PrimitiveElement concreteElementB);
    }
}
