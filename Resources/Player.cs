using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources
{
    public class Player
    {
        public int coins { get; set; }
        public int score { get; set; }
        public float health { get; set; }
        public long id { get; set; }
        public string name { get; set; }
        public long PosX { get; set; }
        public long PosY { get; set; }

        public Player()
        {
            PosX = 0;
            PosY = 0;
            coins = 0;
            score = 0;
            health = 100;
            name = "DefaultName";
        }

        public void SetId(long _id)
        {
            id = _id;
        }
    }
}
