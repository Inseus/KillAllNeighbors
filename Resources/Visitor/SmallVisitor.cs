using KillAllNeighbors.Resources.Composite;
using System;
using System.Drawing;

namespace KillAllNeighbors.Resources.Visitor
{
    class SmallVisitor : IVisitors
    {
        public void VisitCompositeElement(CompositeElement compositeElement)
        {
            CompositeElement element = compositeElement as CompositeElement;
            element.line.Size = new Size(element.SizeX, element.SizeY);
            element.line.BackColor = Color.Green;
        }
        public void VisitPrimitiveElement(PrimitiveElement primitiveElement)
        {
            PrimitiveElement element = primitiveElement as PrimitiveElement;
            element.line.Size = new Size(element.SizeY, element.SizeX);
            element.line.BackColor = Color.Green;
        }
    }
}
