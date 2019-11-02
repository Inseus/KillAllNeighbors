using KillAllNeighbors.Resources.Builder;
using KillAllNeighbors.Resources.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Adapter
{
    interface IUnitsToEnemies
    {
        void ConvertUnitsToEnemies(List<Enemy> enemyList, Form1 form, CreatorOfPictureBox creator, Player thePlayer);
    }
}
