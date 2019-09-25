using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources
{
    public class Player
    {
        Vector2 currentCoord;
        int coins;
        int score;
        float health;
        int id;
        string name;

        public Player()
        {
            currentCoord = new Vector2();
            coins = 0;
            score = 0;
            health = 100;
            name = "AAAAAAAAAAA";
        }

        public void SetId(int _id)
        {
            id = _id;
        }
    }
}
