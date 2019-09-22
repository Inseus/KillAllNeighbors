using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KillAllNeighbors.Resources
{
    public class CoinsHandler
    {
        List<PictureBox> list;
        int coins;
        int numberOfCoins;
        Random seed;
        public CoinsHandler(int numberOfCoincs)
        {
            list = new List<PictureBox>();
            coins = 0;
            this.numberOfCoins = numberOfCoincs;
            seed = new Random();
            spawnCoins(numberOfCoincs);
        }
        void spawnCoins(int number)
        {
            for (int i = 0; i < number; i++)
            {
                makeCoin();
            }
        }
        void makeCoin()
        {
            var newCoin = new PictureBox
            {
                Name = "pictureBox",
                Size = new Size(18, 18),
                Location = new Point(seed.Next(0, 1400), seed.Next(0, 700)),
                BackColor = Color.Yellow,
            };
            list.Add(newCoin);
        }
        public List<PictureBox> getList()
        {
            return list;
        }
        public void increaseCoins()
        {
            coins++;
        }
        public int getCoincs()
        {
            return coins;
        }
        public void relocate(PictureBox model)
        {
            model.Location = new Point(seed.Next(0, 1400), seed.Next(0, 700));
        }
    }
}
