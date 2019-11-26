using KillAllNeighbors.Resources.Adapter;
using KillAllNeighbors.Resources.Builder;
using KillAllNeighbors.Resources.Composite;
using KillAllNeighbors.Resources.Decorator;
using KillAllNeighbors.Resources.State;
using KillAllNeighbors.Resources.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        Timer gameTimer;
        Timer moveTimer;
        Timer requestTimer;
        GameState state = null;

        public Facade(Form1 playerBoard, Player boardPlayer, CreatorOfPictureBox creator, Timer a, Timer b, Timer c)
        {
            state = new StartGame();
            gameTimer = a;
            moveTimer = b;
            requestTimer = c;
            coinsController = new CoinsController();
            creatorOfPictureBox = creator;
            this.playerBoard = playerBoard;
            this.player = boardPlayer;
            connectionHandler = new ConnectionHandler(boardPlayer);
            enemyList = new List<Enemy>();
            gameAction();
        }
        public void setState(GameState state)
        {
            this.state = state;
        }
        public void gameAction()
        {
            state.Handle(connectionHandler);
        }
        public void endGame()
        {
           if(enemyList.Find(x=>x.whoWon!=0)!=null)
            {
                Enemy a = enemyList.Find(x => x.whoWon != 0);
                if (player.id== a.whoWon)
                {
                    playerBoard.LabelText = "You won";
                    stopTimers();
                    setState(new GameOver());
                    gameAction();
                }
                else
                {
                    playerBoard.LabelText = "Game over player" +a.whoWon+" won";
                    stopTimers();

                }
                
            }
        }
        public void stopTimers()
        {
            gameTimer.Enabled = false;
            moveTimer.Enabled = false;
            requestTimer.Enabled = false;
            
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
        public CompositeElement SpawnObstacles()
        {
            CompositeElement obstacle = new CompositeElement("horizontal1", 220, 200, 200, 20);
            obstacle.Add(new PrimitiveElement("vertical1", 400, 200, 20, 200));
            obstacle.Add(new PrimitiveElement("horizontal2", 1000, 380, 200, 20));
            obstacle.Add(new PrimitiveElement("vertical2", 980, 200, 20, 200));
            obstacle.Add(new PrimitiveElement("verticalMiddle1", 610, 0, 20, 200));
            obstacle.Add(new PrimitiveElement("verticalMiddle2", 810, 500, 20, 200));

            return obstacle;


            //Controls.Add(obstacle1.line);
            //for (int i = 0; i < obstacle1.elements.Count; i++)
            //    Controls.Add(obstacle1.elements[i].line);

            //CompositeElement obstacle2 = new CompositeElement("horizontal2", 1000, 380, 200, 20);
            //obstacle2.Add(new PrimitiveElement("vertical2", 980, 200, 20, 200));

            //Controls.Add(obstacle2.line);
            //for (int i = 0; i < obstacle2.elements.Count; i++)
            //    Controls.Add(obstacle2.elements[i].line);

            //PrimitiveElement verticalMiddle1 = new PrimitiveElement("verticalMiddle1", 610, 0, 20, 200);
            //PrimitiveElement verticalMiddle2 = new PrimitiveElement("verticalMiddle2", 810, 500, 20, 200);
            //Controls.Add(verticalMiddle1.line);
            //Controls.Add(verticalMiddle2.line);

        }
        public void HandleConnection()
        {
            if (connectionHandler.connectionEstablished)
            {
                setState(new GameStarted());
                gameAction();
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
