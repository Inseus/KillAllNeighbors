using System;
using System.Drawing;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources.Composite
{
    class PrimitiveElement : DrawingElement
    {
        // Constructor
        public PrimitiveElement(string name, int posX, int posY,int sizeX, int sizeY)
          : base(name,posX,posY,sizeX,sizeY)
        {
            line = new PictureBox();
            line.Size = new Size(SizeY, SizeX);
            line.BackColor = Color.Goldenrod;
            line.Location = new Point(PosX, PosY);
        }
        public override void Add(DrawingElement c)
        {
            Console.WriteLine(
              "Cannot add to a PrimitiveElement");
        }



        public override void Remove(DrawingElement c)
        {
            Console.WriteLine(
              "Cannot remove from a PrimitiveElement");
        }
        public override void Display(int indent)
        {
            Console.WriteLine(
              new String('-', indent) + " " + _name);
        }
    }
}
