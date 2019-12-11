using KillAllNeighbors.Resources.Builder;
using KillAllNeighbors.Resources.Decorator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Adapter
{
    class UnitsToEnemiesAdapter : IUnitsToEnemies
    {
        ConnectionHandler connection;
        public UnitsToEnemiesAdapter(ConnectionHandler connection)
        {
            this.connection = connection;
        }
        public async void ConvertUnitsToEnemies( List<Enemy> enemyList, Form1 form, CreatorOfPictureBox creator, Player thePlayer)
        {
            ICollection<Unit> playerCollection = await connection.GetAllPlayersData();
            if (playerCollection == null)
            {
                return;
            }
            if (playerCollection.Count ==0 )
            {
                return;
            }
            Unit tempUnit = playerCollection.Where(player => player.id == thePlayer.id).ToList()[0];

            foreach (var item in playerCollection)
            {
                if (item.id == thePlayer.id)
                {
                    thePlayer.message = item.message;
                    break;
                }
            }

            playerCollection = playerCollection.Where(player => player.id != thePlayer.id).ToList();

            if (tempUnit.isShooting==1)
            {
                thePlayer.isShooting = 0;
            }
            // Surandam dar nesancius zaidejus ir pridedam
            foreach (Unit p in playerCollection)
            {  
                if (enemyList.Find(x => x.id == p.id) == null)
                {
                    Enemy temp = new Enemy(creator, p.id);
                    temp.addToForm(form);
                    enemyList.Add(temp);
                    
                }
                else
                {
                    enemyList.Find(x => x.id == p.id).getMovableObject().Location = new Point((int)p.PosX, (int)p.PosY);
                    enemyList.Find(x => x.id == p.id).shootingType = p.shootingType;
                    enemyList.Find(x => x.id == p.id).isShooting = p.isShooting;
                    enemyList.Find(x => x.id == p.id).facing = p.facing;
                    enemyList.Find(x => x.id == p.id).whoWon = p.whoWon;

                }
            }
        }

    }
}
