using KillAllNeighbors.Resources;
using KillAllNeighbors.Resources.Builder;
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
        private PictureBox playerObject;
        private CoinsController coinsController;
        private static readonly object lockObject = new object();
        private int coinSpawnInterval = 300;
        private int moveInterval = 30;
        private int requestInterval = 100;
        public List<PlayerWithObject> enemyList;
        public PlayerWithObject thisPlayer;
        PictureBoxBuilder builder;
        CreatorOfPictureBox creator;

        delegate void AddOrRemoveToControl(Coin coin);
        delegate void GetVector();

        public Form1()
        {
            InitializeComponent();
        }
        public async void Connect()
        {
            await connectionHandler.Connect();
        }
        public async Task CreateEnemyAndUpdateEnemy()
        {

            ICollection<Player> playerCollection = await connectionHandler.GetAllPlayersData();
            if (playerCollection == null)
            {
                return;
            }

            playerCollection = playerCollection.Where(player => player.id != this.thisPlayer.player.id).ToList();
            // Surandam dar nesancius zaidejus ir pridedam
            foreach (Player p in playerCollection)
            {
                label2.Text = p.message;
                //if (p.coins % 10 == 0 && p.coins != 0)
                    // label2.Text = "Player " + p.id.ToString() + " collected " + p.coins.ToString() + " coins";   

                if (enemyList.Find(x => x.player.id == p.id) == null)
                {
                    label2.Text = "";
                    builder = new EnemyBoxBuilder();
                    creator.Construct(builder);
                    var box = builder.GetResult();
                    this.Controls.Add(box);
                    PlayerWithObject temp = new PlayerWithObject(box, p);
                    enemyList.Add(temp);
                }
                else
                {
                    enemyList.Find(x => x.player.id == p.id).movableObject.Location = new Point((int)p.PosX, (int)p.PosY);

                }

            }

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
                connectionHandler.UpdatePlayerData(playerObject.Location.X, playerObject.Location.Y);
                CreateEnemyAndUpdateEnemy();
            }

        }

        private void HandleMoveTimerTick(object sender, EventArgs e)
        {
            lock (lockObject)
            {
                TryMove();
                TryCollectCoin();
                //TryShoot();
                Collisions();
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            string direction = ControlsHandler.Instance.GetDirection();
            if (Keyboard.IsKeyDown(Key.NumPad1) || Keyboard.IsKeyDown(Key.NumPad2) || Keyboard.IsKeyDown(Key.NumPad3))
            {
                this.Controls.Add(playerObject);
                // this is the function thats makes the new bullets in this game
                Bullet typeOfBullet = ControlsHandler.Instance.GetWeapon(creator);
                if (typeOfBullet != null)
                {
                    
                    typeOfBullet.direction = direction; // assignment the direction to the bullet
                    typeOfBullet.bulletLeft = playerObject.Left + (playerObject.Width / 2); // place the bullet to left half of the player
                    typeOfBullet.bulletTop = playerObject.Top + (playerObject.Height / 2); // place the bullet on top half of the player
                    typeOfBullet.mkBullet(this); // run the function mkBullet from the bullet class. 
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        //private void TryShoot()
        //{

        //    this.Controls.Add(playerObject);
        //        string direction = ControlsHandler.Instance.GetDirection();
        //    // this is the function thats makes the new bullets in this game
        //    Bullet typeOfBullet = ControlsHandler.Instance.GetWeapon(creator);
        //    if (typeOfBullet != null)
        //    {
        //        typeOfBullet.direction = direction; // assignment the direction to the bullet
        //        typeOfBullet.bulletLeft = playerObject.Left + (playerObject.Width / 2); // place the bullet to left half of the player
        //        typeOfBullet.bulletTop = playerObject.Top + (playerObject.Height / 2); // place the bullet on top half of the player
        //        typeOfBullet.mkBullet(this); // run the function mkBullet from the bullet class. 
        //    }


        //}
        private void HandleTimerTick(object sender, EventArgs e)
        {
            Coin _spawnedCoin = coinsController.SpawnNewCoin();
            ControlCoins(_spawnedCoin);
        }

        void ControlForm(Coin coin)
        {
            if (coin == null)
                return;
            if (Controls.Contains(coin.GetFormControlItem()))
            {
                Controls.Remove(coin.GetFormControlItem());
                label1.Text = "Coins: " + CoinsHandler.Instance.GetCoinsCount();
                thisPlayer.player.coins = CoinsHandler.Instance.GetCoinsCount();
                return;
            }
            else
            {
                Controls.Add(coin.GetFormControlItem());
                return;
            }
        }

        void ControlCoins(Coin tempCoin)
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
            if (playerObject.Location.X + _temp.x >= Constants.MIN_BOUND_X && playerObject.Location.Y + _temp.y >= Constants.MIN_BOUND_Y)
            {

                playerObject.Location = new Point(playerObject.Location.X + _temp.x, playerObject.Location.Y + _temp.y);
            }
        }

        private void TryCollectCoin()
        {
            Coin _temp = CoinsHandler.Instance.TryCollectCoin(playerObject, coinsController.GetCoinList());
            if (_temp != null)
            {
                coinsController.RemoveCoin(_temp);
                ControlCoins(_temp);
            }
        }


        private void PictureBox1_Click(object sender, EventArgs e)
        {
            // Do nothing
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
            foreach (PlayerWithObject x in enemyList)
            {
                // below is the second for loop, this is nexted inside the first one
                // the bullet and zombie needs to be different than each other
                // then we can use that to determine if the hit each other
                foreach (Control j in this.Controls)
                {
                    // below is the selection thats identifying the bullet and zombie

                    if ((j is PictureBox && j.Name == "bullet") && (x.movableObject is PictureBox && x.movableObject.Name == "enemy"))
                    {
                        // below is the if statement thats checking if bullet hits the zombie
                        if (x.movableObject.Bounds.IntersectsWith(j.Bounds))
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
            // Sukuriama kurėją
            creator = new CreatorOfPictureBox();
            // Kurėjas sukuria įrankį skirtą konstruoti zaidėjo pictur boxa
            builder = new PlayerBoxBuilder();
            // Sukuriamas žaidėjo pictur boxas
            creator.Construct(builder);
            playerObject = builder.GetResult();
            this.Controls.Add(playerObject);
            thisPlayer = new PlayerWithObject(playerObject, new Player());
            enemyList = new List<PlayerWithObject>();
            this.MinimumSize = new Size(Constants.VIEW_SIZE_X, Constants.VIEW_SIZE_Y);
            this.AutoScrollPosition = playerObject.Location;
            gameTimer = new Timer { Interval = coinSpawnInterval };
            moveTimer = new Timer { Interval = moveInterval };
            requestTimer = new Timer { Interval = requestInterval };
            coinsController = new CoinsController();
            connectionHandler = new ConnectionHandler(thisPlayer.player);
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
