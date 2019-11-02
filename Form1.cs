using KillAllNeighbors.Resources;
using KillAllNeighbors.Resources.Adapter;
using KillAllNeighbors.Resources.Builder;
using KillAllNeighbors.Resources.Decorator;
using KillAllNeighbors.Resources.Strategy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace KillAllNeighbors
{
    public partial class Form1 : Form
    {
        // Interval 1000 = 1 sekunde, kuo didesnis skaicius, tuo leciau viskas vyks
        private ConnectionHandler connectionHandler;
        private Timer gameTimer;
        private Timer moveTimer;
        private Timer requestTimer;
        private Vector2 _temp = new Vector2();
        private CoinsController coinsController;
        private static readonly object lockObject = new object();
        private int coinSpawnInterval = 300;
        private int moveInterval = 30;
        private int requestInterval = 100;
        public List<Enemy> enemyList = new List<Enemy>();
        public Player thisPlayer;
        CreatorOfPictureBox creator;

        delegate void AddOrRemoveToControl(ICurrency coin);
        delegate void GetVector();

        public Form1()
        {
            InitializeComponent();
        }
        public async void Connect()
        {
            await connectionHandler.Connect();
        }
        public void UpdateEnemyListFromServer()
        {
            // Adapter design pattern
            IUnitsToEnemies enemies = new UnitsToEnemiesAdapter(connectionHandler);
            enemies.ConvertUnitsToEnemies(enemyList, this, creator, thisPlayer);
        }
        private void AddEvents()
        {
            gameTimer.Tick += HandleTimerTick;
            moveTimer.Tick += HandleMoveTimerTick;
            requestTimer.Tick += HandleRequestTick;
            this.FormClosing += AppClose;
        }
        private void HandleRequestTick(object sender, EventArgs e)
        {
            if (connectionHandler.connectionEstablished)
            {
                connectionHandler.UpdatePlayerData();
                UpdateEnemyListFromServer();
                EnemiesShooting();
            }

        }
        private void HandleMoveTimerTick(object sender, EventArgs e)
        {
            lock (lockObject)
            {
                TryMove();
                TryCollectCoin();
                //TryShoot();
                //Collisions();
            }
        }
        private void EnemiesShooting()
        {
            foreach (var enemy in enemyList)
            {
                if(enemy.isShooting==1)
                {
                    Bullet typeOfBullet = ControlsHandler.Instance.GetWeaponEnemy(creator,enemy.shootingType);
                    if (typeOfBullet != null)
                    {
                        typeOfBullet.direction = enemy.facing;
                        typeOfBullet.bulletLeft = enemy.getMovableObject().Left
                            + (enemy.getMovableObject().Width / 2); // place the bullet to left half of the player
                        typeOfBullet.bulletTop = enemy.getMovableObject().Top +
                            (enemy.getMovableObject().Height / 2); // place the bullet on top half of the player
                        typeOfBullet.mkBullet(this); // run the function mkBullet from the bullet class. 
                    }
                }
            }         
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            thisPlayer.facing = ControlsHandler.Instance.GetDirection();
            if (Keyboard.IsKeyDown(Key.NumPad1) || Keyboard.IsKeyDown(Key.NumPad2) || Keyboard.IsKeyDown(Key.NumPad3))
            {
                thisPlayer.isShooting = 1;
                // this is the function thats makes the new bullets in this game

                Bullet typeOfBullet = ControlsHandler.Instance.GetWeapon(creator, thisPlayer);
                if (typeOfBullet != null)
                {
                    typeOfBullet.direction = thisPlayer.facing; // assignment the direction to the bullet
                    typeOfBullet.bulletLeft = thisPlayer.getMovableObject().Left
                        + (thisPlayer.getMovableObject().Width / 2); // place the bullet to left half of the player
                    typeOfBullet.bulletTop = thisPlayer.getMovableObject().Top +
                        (thisPlayer.getMovableObject().Height / 2); // place the bullet on top half of the player
                    typeOfBullet.mkBullet(this); // run the function mkBullet from the bullet class. 
                }

                //Bullet typeOfBullet1 = ControlsHandler.Instance.GetWeapon(creator);
                //if (typeOfBullet1 != null)
                //{
                //PictureBox imenemy = enemyList[enemyList.Count - 1].getMovableObject();
                //typeOfBullet1.direction = direction; // assignment the direction to the bullet
                //typeOfBullet1.bulletLeft = imenemy.Left
                //+(imenemy.Width / 2); // place the bullet to left half of the player
                //typeOfBullet1.bulletTop = imenemy.Top +
                //        (imenemy.Height / 2); // place the bullet on top half of the player

                //typeOfBullet1.mkBullet(this); // run the function mkBullet from the bullet class. 
                //}
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void HandleTimerTick(object sender, EventArgs e)
        {
            ICurrency _spawnedCoin = coinsController.SpawnNewCoin();
            ControlCoins(_spawnedCoin);
        }
        void ControlForm(ICurrency coin)
        {
            if (coin == null)
                return;
            if (Controls.Contains(coin.controlItem))
            {
                Controls.Remove(coin.controlItem);
                label1.Text = "Coins: " + CoinsHandler.Instance.GetCoinsCount();
                thisPlayer.coins = CoinsHandler.Instance.GetCoinsCount();
                return;
            }
            else
            {
                Controls.Add(coin.controlItem);
                return;
            }
        }
        void ControlCoins(ICurrency tempCoin)
        {
            try
            {
                this.Invoke((AddOrRemoveToControl)delegate
                {
                    ControlForm(tempCoin);
                }, tempCoin);
            }
            catch (Exception e)
            {
                Console.WriteLine("Thread was spawning elements on disposed object");
            }
        }
        private void TryMove()
        {
            _temp = ControlsHandler.Instance.GetVector();
            if (thisPlayer.getMovableObject().Location.X + _temp.x >= Constants.MIN_BOUND_X && thisPlayer.getMovableObject().Location.Y + _temp.y >= Constants.MIN_BOUND_Y)
            {

                thisPlayer.getMovableObject().Location = new Point(thisPlayer.getMovableObject().Location.X + 
                    _temp.x, thisPlayer.getMovableObject().Location.Y + _temp.y);
                thisPlayer.setCordinatesFromPictureBoxToPlayer();
            }
        }
        private void TryCollectCoin()
        {
            ICurrency _temp = CoinsHandler.Instance.TryCollectCoin(thisPlayer.getMovableObject(), coinsController.GetCoinList());
            if (_temp != null)
            {
                coinsController.RemoveCoin(_temp);
                ControlCoins(_temp);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetValues();
            moveTimer.Start();
            gameTimer.Start();
            AddEvents();
            Connect();
            requestTimer.Start();
            
        }
        private void Collisions()
        {
            // run the first for each loop below
            // X is a control and we will search for all controls in this loop
            foreach (Enemy x in enemyList)
            {
                // below is the second for loop, this is nexted inside the first one
                // the bullet and zombie needs to be different than each other
                // then we can use that to determine if the hit each other
                foreach (Control j in this.Controls)
                {
                    // below is the selection thats identifying the bullet and zombie

                    if ((j is PictureBox && j.Name == "bullet") && (x.getMovableObject() is PictureBox && x.getMovableObject().Name == "enemy"))
                    {
                        // below is the if statement thats checking if bullet hits the zombie
                        if (x.getMovableObject().Bounds.IntersectsWith(j.Bounds))
                        {
                            this.Controls.Remove(j); // this will remove the bullet from the screen
                            j.Dispose(); // this will dispose the bullet all together from the program
                            //x.hit();
                            //if(!x.isAlive())
                            //{
                            //    this.Controls.Remove(x.movableObject); // this will remove the zombie from the screen
                            //    x.movableObject.Dispose(); // this will dispose the zombie from the program
                            //}

                        }
                    }
                }
            }
        }
        private void SetValues()
        {
            creator = new CreatorOfPictureBox();
            thisPlayer = new Player(creator);
            thisPlayer.addToForm(this);
            this.MinimumSize = new Size(Constants.VIEW_SIZE_X, Constants.VIEW_SIZE_Y);
            this.AutoScrollPosition = thisPlayer.getMovableObject().Location;
            gameTimer = new Timer { Interval = coinSpawnInterval };
            moveTimer = new Timer { Interval = moveInterval };
            requestTimer = new Timer { Interval = requestInterval };
            coinsController = new CoinsController();
            connectionHandler = new ConnectionHandler(thisPlayer);
        }
        private void AppClose(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            gameTimer.Enabled = false;
            moveTimer.Enabled = false;
            this.Dispose();
            e.Cancel = false;
        }
    }
}
