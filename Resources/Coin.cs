using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources
{
    public class Coin
    {
        PictureBox coin = new PictureBox();
        int value = 1;

        public Coin(int value, Random seed, int sizeX = 18, int sizeY = 18)
        {
            this.value = value;
            coin.Size = new Size(sizeX, sizeY);
            coin.BackColor = Color.Yellow;
            coin.Location = new Point(seed.Next(0, Constants.VIEW_SIZE_X), seed.Next(0, Constants.VIEW_SIZE_Y));
        }

        public PictureBox GetPicture()
        {
            return coin;
        }

        public void SetNewColor(Color color)
        {
            coin.BackColor = color;
        }
    }
}
