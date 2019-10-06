using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources
{
    public class PlayerWithObject
    {
        public PictureBox movableObject { get; set; }

        public Player player { get; set; }

        public PlayerWithObject(PictureBox movableObject, Player player)
        {

            this.movableObject = movableObject;
            this.player = player;
        }

        public void hit()
        {
            player.health = player.health - 20;
        }
        public bool isAlive()
        {
            if (player.health < 0)
                return false;
            return true;

        }
    }
}

