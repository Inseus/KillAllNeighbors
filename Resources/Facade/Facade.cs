using KillAllNeighbors.Resources.Adapter;
using KillAllNeighbors.Resources.Builder;
using KillAllNeighbors.Resources.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Facade
{
    public class Facade
    {
        protected ConnectionHandler connectionHandler;
        protected CoinsController coinsController;
        protected CreatorOfPictureBox creatorOfPictureBox;
        protected Form1 playerBoard;
        protected Player player;
        protected List<Enemy> enemyList;

        public Facade(Form1 playerBoard, Player boardPlayer, CreatorOfPictureBox creator)
        {
            coinsController = new CoinsController();
            creatorOfPictureBox = creator;
            this.playerBoard = playerBoard;
            this.player = boardPlayer;
            connectionHandler = new ConnectionHandler(boardPlayer);
            enemyList = new List<Enemy>();
            Connect();
        }
        private async void Connect()
        {
            await connectionHandler.Connect();
        }

        private void UpdateEnemyListFromServer()
        {
            // Adapter design pattern
            IUnitsToEnemies enemies = new UnitsToEnemiesAdapter(connectionHandler);
            enemies.ConvertUnitsToEnemies(enemyList, playerBoard, creatorOfPictureBox, player);
        }

        private void EnemiesShooting()
        {
            foreach (var enemy in enemyList)
            {
                if (enemy.isShooting == 1)
                {
                    Bullet typeOfBullet = ControlsHandler.Instance.GetWeaponEnemy(creatorOfPictureBox, enemy.shootingType);
                    if (typeOfBullet != null)
                    {
                        typeOfBullet.direction = enemy.facing;
                        typeOfBullet.bulletLeft = enemy.getMovableObject().Left
                            + (enemy.getMovableObject().Width / 2); // place the bullet to left half of the player
                        typeOfBullet.bulletTop = enemy.getMovableObject().Top +
                            (enemy.getMovableObject().Height / 2); // place the bullet on top half of the player
                        typeOfBullet.mkBullet(playerBoard); // run the function mkBullet from the bullet class. 
                    }
                }
            }
        }

        public void HandleConnection()
        {
            if (connectionHandler.connectionEstablished)
            {
                connectionHandler.UpdatePlayerData();
                UpdateEnemyListFromServer();
                EnemiesShooting();
            }
        }

        public void RemoveCoin(ICurrency coin)
        {
            coinsController.RemoveCoin(coin);
        }

        public ICurrency SpawnCoin()
        {
            return coinsController.SpawnNewCoin();
        }

        public List<ICurrency> GetCoinList()
        {
            return coinsController.GetCoinList();
        }

        public List<Enemy> GetEnemyList()
        {
            return enemyList;
        }
    }
}
