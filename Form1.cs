using KillAllNeighbors.Resources;
using KillAllNeighbors.Resources.Adapter;
using KillAllNeighbors.Resources.Builder;
using KillAllNeighbors.Resources.Command;
using KillAllNeighbors.Resources.Composite;
using KillAllNeighbors.Resources.Decorator;
using KillAllNeighbors.Resources.Facade;
using KillAllNeighbors.Resources.Strategy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private Timer gameTimer;
        private Timer moveTimer;
        private Timer requestTimer;
        private static readonly object lockObject = new object();

        private int coinSpawnInterval = 300;
        private int moveInterval = 30;
        private int requestInterval = 80;
        private Player thisPlayer;
        private Facade formControls;
        private CreatorOfPictureBox creatorOfPictureBox;
        private Receiver receiver = new Receiver();
        delegate void AddOrRemoveToControl(ICurrency coin);
        delegate void GetVector();

        public Form1()
        {
            InitializeComponent();
        }

        private void AddEvents()
        {
            obstacleCreation();
            gameTimer.Tick += HandleCoinsControlTick;
            moveTimer.Tick += HandleMoveTimerTick;
            requestTimer.Tick += HandleRequestTick;
            this.FormClosing += AppClose;
        }

        
        private void HandleRequestTick(object sender, EventArgs e)
        {
            formControls.HandleConnection();
            formControls.endGame();
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
        void obstacleCreation()
        {
            CompositeElement obstacle = formControls.SpawnObstacles();

            Controls.Add(obstacle.line);
            for (int i = 0; i < obstacle.elements.Count; i++)
                Controls.Add(obstacle.elements[i].line);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            thisPlayer.facing = ControlsHandler.Instance.GetDirection();
            if (Keyboard.IsKeyDown(Key.NumPad1) || Keyboard.IsKeyDown(Key.NumPad2) || Keyboard.IsKeyDown(Key.NumPad3))
            {
                thisPlayer.isShooting = 1;
                // this is the function thats makes the new bullets in this game

                Bullet typeOfBullet = ControlsHandler.Instance.GetWeapon(creatorOfPictureBox, thisPlayer);
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
        private void HandleCoinsControlTick(object sender, EventArgs e)
        {
            ControlCoins(formControls.SpawnCoin());
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
            Vector2 _tempVec = ControlsHandler.Instance.GetVector();
            
            if (thisPlayer.getMovableObject().Location.X + _tempVec.x >= Constants.MIN_BOUND_X && thisPlayer.getMovableObject().Location.Y + _tempVec.y >= Constants.MIN_BOUND_Y)
            {
                CompositeElement obstacle = formControls.SpawnObstacles();

                //check if new player position intersects with an obstacle
                //root obstacle
                if(!obstacle.line.Bounds.IntersectsWith(new Rectangle (new Point(thisPlayer.getMovableObject().Location.X + _tempVec.x, thisPlayer.getMovableObject().Location.Y + _tempVec.y),thisPlayer.getMovableObject().Bounds.Size)))
                {
                    int intersections = 0;
                    for (int i = 0; i < obstacle.elements.Count; i++)
                    {
                        //all the other obstacles
                        if (obstacle.elements[i].line.Bounds.IntersectsWith(new Rectangle(new Point(thisPlayer.getMovableObject().Location.X + _tempVec.x, thisPlayer.getMovableObject().Location.Y + _tempVec.y), thisPlayer.getMovableObject().Bounds.Size)))
                        {
                            intersections++;
                        }
                    }
                    //if it doesn't intersect the root obstacle or all the other obstacles
                    if(intersections == 0)
                        Invoker.AddCommand(new ConcreteCommand(receiver, _tempVec.x, _tempVec.y, thisPlayer));
                }
                //old moving               
                //Resources.Command.ICommand command = new ConcreteCommand(_tempVec.x, _tempVec.y,thisPlayer);
                //Invoker.AddCommand(_tempVec.x, _tempVec.y, thisPlayer);
                //thisPlayer.getMovableObject().Location = new Point(thisPlayer.getMovableObject().Location.X + 
                //    _tempVec.x, thisPlayer.getMovableObject().Location.Y + _tempVec.y);
                //thisPlayer.setCordinatesFromPictureBoxToPlayer();

            }
        }
        private void TryCollectCoin()
        {

            ICurrency _temp = formControls.GetCollidingCoin(thisPlayer.getMovableObject());
            if (_temp != null)
            {
                formControls.RemoveCoin(_temp);
                ControlCoins(_temp);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetValues();
            moveTimer.Start();
            gameTimer.Start();
            AddEvents();
            requestTimer.Start();
        }
        private void Collisions()
        {
            // run the first for each loop below
            // X is a control and we will search for all controls in this loop
            foreach (Enemy x in formControls.GetEnemyList())
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
            creatorOfPictureBox = new CreatorOfPictureBox();
            thisPlayer = new Player(creatorOfPictureBox);
            

            this.MinimumSize = new Size(Constants.VIEW_SIZE_X, Constants.VIEW_SIZE_Y);
            this.AutoScrollPosition = thisPlayer.getMovableObject().Location;

            gameTimer = new Timer { Interval = coinSpawnInterval };
            moveTimer = new Timer { Interval = moveInterval };
            requestTimer = new Timer { Interval = requestInterval };
            formControls = new Facade(this, thisPlayer, creatorOfPictureBox, gameTimer, moveTimer, requestTimer);
            InitializePlayer();
        }

        public void InitializePlayer()
        {
            thisPlayer.addToForm(this);
        }
        private void AppClose(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            gameTimer.Enabled = false;
            moveTimer.Enabled = false;
            this.Dispose();
            e.Cancel = false;
        }
        public string LabelText
        {
            get
            {
                return this.label2.Text;
            }
            set
            {
                this.button1.Visible = true;
                this.label2.Visible = true;
                this.label2.Text = value;
            }
        }
        public void startNewGame()
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath); // to start new instance of application
            this.Close(); //to turn off current app
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startNewGame();
        }
    }
}
