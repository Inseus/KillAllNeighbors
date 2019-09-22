using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;




namespace KillAllNeighbors.Resources
{
    public class CoincsHandler
    {
        List<PictureBox> list;
        int coincs;
        int numberOfCoincs;
        Random seed;
        public CoincsHandler(int numberOfCoincs)
        {
            list = new List<PictureBox>();
            coincs = 0;
            this.numberOfCoincs = numberOfCoincs;
            seed = new Random();
            makeNumberOfCoincs(numberOfCoincs);
        }
        void makeNumberOfCoincs(int number)
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
        public void increaseCoincs()
        {
            coincs++;
        }
        public int getCoincs()
        {
            return coincs;
        }
        public void relocate(PictureBox model)
        {
            model.Location = new Point(seed.Next(0, 1400), seed.Next(0, 700));
        }
    }
}
