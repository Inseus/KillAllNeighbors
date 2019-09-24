using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources
{
    /// <summary>
    /// Basis coin class:
    /// Can set value, color, size etc.
    /// </summary>
    public class Coin
    {
        PictureBox controlItem = new PictureBox();
        int value = 1;

        public Coin(int value, Random seed, int sizeX = 18, int sizeY = 18)
        {
            this.value = value;
            controlItem.Size = new Size(sizeX, sizeY);
            controlItem.BackColor = Color.Yellow;
            controlItem.Location = new Point(seed.Next(0, Constants.VIEW_SIZE_X), seed.Next(0, Constants.VIEW_SIZE_Y));
        }

        public PictureBox GetFormControlItem()
        {
            return controlItem;
        }

        public int GetValue()
        {
            return this.value;
        }

        public void SetNewColor(Color color)
        {
            controlItem.BackColor = color;
        }
    }
}
