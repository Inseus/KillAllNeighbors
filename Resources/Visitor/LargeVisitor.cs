using KillAllNeighbors.Resources.Composite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Visitor
{
    class LargeVisitor : IVisitors
    {
        public void VisitCompositeElement(CompositeElement compositeElement)
        {
            CompositeElement element = compositeElement as CompositeElement;
            element.line.Size = new Size(element.SizeX * 2, element.SizeY);
            element.line.BackColor = Color.Black;
        }
        public void VisitPrimitiveElement(PrimitiveElement primitiveElement)
        {
            PrimitiveElement element = primitiveElement as PrimitiveElement;
            element.line.Size = new Size(element.SizeY * 2, element.SizeX);
            element.line.BackColor = Color.Black;
        }
    }
}
