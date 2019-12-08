using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources.Decorator
{
    public class Unit
    {       
        public int coins { get; set; }
        public int score { get; set; }
        public float health { get; set; }
        public long id { get; set; }
        public string name { get; set; }
        public long PosX { get; set; }
        public long PosY { get; set; }       
        public string message { get; set; }

        public int whoWon { get; set; }
        public int isShooting { get; set; }

        public int shootingType { get; set; }

        public string facing { get; set; }
        public Unit()
        {
            isShooting = 0;
            shootingType = 0;
            facing = "down";
            PosX = 0;
            PosY = 0;
            coins = 0;
            score = 0;
            health = 100;
            name = "DefaultName";
            message = "";
                whoWon = 0;
        }
        public void SetId(long _id)
        {
            id = _id;
        }
        public void hit()
        {
            coins = coins - 10;
        }
    }
}
