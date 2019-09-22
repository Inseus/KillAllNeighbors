using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources
{
    public class CoinsController
    {
        private int maxCoins = 10;
        private int currentCoins = 0;
        Random seed;

        List<PictureBox> coinList = new List<PictureBox>();

        public CoinsController CoinSpawner
        {

        }
        public void coinSpawner(int maxCoins, int currentCoins)
        {
            if (currentCoins < maxCoins)
                coinMaker();
        }
        void coinMaker()
        {
            var newCoin = new PictureBox
            {
                Name = "pictureBox",
                Size = new Size(18, 18),
                Location = new Point(seed.Next(0, 1400), seed.Next(0, 700)),
                BackColor = Color.Yellow,
            };
            coinList.Add(newCoin);
                
        }
    }
}
