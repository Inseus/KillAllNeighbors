using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources.Composite
{
    public class CompositeElement : DrawingElement
    {
        public List<DrawingElement> elements = new List<DrawingElement>();

        // Constructor
        public CompositeElement(string name, int posX, int posY, int sizeX, int sizeY)
          : base(name,posX,posY,sizeX,sizeY)
        {
            line = new PictureBox();
            line.Size = new Size(SizeX, SizeY);
            line.BackColor = Color;
            line.Location = new Point(PosX, PosY);
        }
        public override void Add(DrawingElement d)
        {
            elements.Add(d);
        }
        public override void Remove(DrawingElement d)
        {
            elements.Remove(d);
        }
        public override void Display(int indent)
        {
            // Display each child element on this node
            foreach (DrawingElement d in elements)
            {
                d.Display(indent + 2);
            }
        }
    }
}
