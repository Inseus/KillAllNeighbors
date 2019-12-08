﻿using KillAllNeighbors.Resources.Adapter;
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
                        typeOfBullet.changeName("enemyBullet");
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
            obstacle.Add(new PrimitiveElement("vertical1", 400, 200, 200, 20));

            CompositeElement obstacle2 = new CompositeElement("horizontal2", 1000, 380, 200, 20);
            obstacle2.Add(new PrimitiveElement("vertical2", 980, 200, 200, 20));

            PrimitiveElement verticalMiddle1 = new PrimitiveElement("verticalMiddle1", 610, 0, 200, 20);
            PrimitiveElement verticalMiddle2 = new PrimitiveElement("verticalMiddle2", 810, 500, 200, 20);

            obstacle.Add(obstacle2);
            obstacle.Add(obstacle2.elements[0]);
            obstacle.Add(verticalMiddle1);
            obstacle.Add(verticalMiddle2);
            obstacle.Display(1);
            return obstacle;

        }

        private bool isStarted = false;
        public void HandleConnection()
        {
            if (connectionHandler.connectionEstablished)
            {
                if (!isStarted)
                {
                    setState(new GameStarted());
                    isStarted = true;
                }
                gameAction();
                UpdateEnemyListFromServer();
                EnemiesShooting();
            }
        }
        public void Collisions()
        {
            // run the first for each loop below
            // X is a control and we will search for all controls in this loop
            foreach (Enemy x in GetEnemyList())
            {
                // below is the second for loop, this is nexted inside the first one
                // the bullet and zombie needs to be different than each other
                // then we can use that to determine if the hit each other
                foreach (Control j in playerBoard.Controls)
                {
                    // below is the selection thats identifying the bullet and zombie

                    if ((j is PictureBox && j.Name == "bullet") && (x.getMovableObject() is PictureBox && x.getMovableObject().Name == "enemy"))
                    {
                        // below is the if statement thats checking if bullet hits the zombie
                        if (x.getMovableObject().Bounds.IntersectsWith(j.Bounds))
                        {
                            playerBoard.Controls.Remove(j); // this will remove the bullet from the screen
                            j.Dispose(); // this will dispose the bullet all together from the program
                            //x.hit();
                            //if(!x.isAlive())
                            //{
                            //    this.Controls.Remove(x.movableObject); // this will remove the zombie from the screen
                            //    x.movableObject.Dispose(); // this will dispose the zombie from the program
                            //}

                        }
                    }
                    if ((j is PictureBox && j.Name == "enemyBullet"))
                    {
                        // below is the if statement thats checking if bullet hits the zombie
                        if (player.getMovableObject().Bounds.IntersectsWith(j.Bounds))
                        {
                            playerBoard.Controls.Remove(j); // this will remove the bullet from the screen
                            j.Dispose(); // this will dispose the bullet all together from the program
                            coinsController.restoreCoins();
                            //if(!x.isAlive())
                            //{
                            //    this.Controls.Remove(x.movableObject); // this will remove the zombie from the screen
                            //    x.movableObject.Dispose(); // this will dispose the zombie from the programD
                            //}

                        }
                    }
                }
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

        public ICurrency GetCollidingCoin(PictureBox moveableObj)
        {
            return coinsController.GetCollidingCoin(moveableObj);
        }


        public List<Enemy> GetEnemyList()
        {
            return enemyList;
        }
       
    }
}
