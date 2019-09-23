using KillAllNeighbors.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private Vector2 _temp = new Vector2();
        private PictureBox moveableObject;
        private CoinsController coinsController;
        private static readonly object lockObject = new object();
        private int coinSpawnInterval = 300;
        private int moveInterval = 15;

        delegate void AddOrRemoveToControl(Coin coin);
        delegate void GetVector();

        public Form1()
        {
            InitializeComponent();
        }

        private void AddEvents()
        {
            gameTimer.Tick += HandleTimerTick;
            moveTimer.Tick += HandleMoveTimerTick;
            this.FormClosing += AppClose;
        }

        private void HandleMoveTimerTick(object sender, EventArgs e)
        {
            lock (lockObject)
            {
                TryMove();
                TryCollectCoin();
            }
        }

        private void HandleTimerTick(object sender, EventArgs e)
        {
            Coin _spawnedCoin = coinsController.SpawnNewCoin();
            ControlCoins(_spawnedCoin);
        }

        void ControlForm(Coin coin)
        {
            if(coin == null)
                return;
            if (Controls.Contains(coin.GetFormControlItem()))
            {
                Controls.Remove(coin.GetFormControlItem());
                label1.Text = "Coins: " + CoinsHandler.Instance.GetCoinsCount();
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
            if (moveableObject.Location.X + _temp.x >= Constants.MIN_BOUND_X && moveableObject.Location.Y + _temp.y >= Constants.MIN_BOUND_Y)
            {
                moveableObject.Location = new Point(moveableObject.Location.X + _temp.x, moveableObject.Location.Y + _temp.y);
            }
        }

        private void TryCollectCoin()
        {
            Coin _temp = CoinsHandler.Instance.TryCollectCoin(moveableObject, coinsController.GetCoinList());
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
        }

        private void SetValues()
        {
            this.MinimumSize = new Size(Constants.VIEW_SIZE_X, Constants.VIEW_SIZE_Y);
            moveableObject = pictureBox1;
            this.AutoScrollPosition = moveableObject.Location;
            gameTimer = new Timer { Interval = coinSpawnInterval };
            moveTimer = new Timer { Interval = moveInterval };
            coinsController = new CoinsController();
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
