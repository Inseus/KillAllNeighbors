using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources.Prototype
{
    class CoinPrototype : ICurrency
    {
        public int Value { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public Color Color { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public PictureBox controlItem { get; set; }
        public CoinPrototype(int posX, int posY, int value, Color color)
        {
            Value = value;
            PosX = posX;
            PosY = posY;
            Color = color;
            SetSize();
            SetControlItem();
        }
        void SetSize()
        {
            SizeX = Constants.COINS_SIZE_MULTIPLIER * Value;
            SizeY = SizeX;
        }

        void SetControlItem()
        {
            controlItem = new PictureBox();
            controlItem.Size = new Size(SizeX, SizeY);
            controlItem.BackColor = Color;
            controlItem.Location = new Point(PosX, PosY);
        }
        public CoinPrototype Clone()
        {
            return (CoinPrototype)this.MemberwiseClone();
        }
    }
}
