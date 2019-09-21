﻿using System;
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
        Timer gameTimer = new Timer { Interval = 20};
        Vector2 _temp = new Vector2();
        PictureBox moveableObject;
        public Form1()
        {
            InitializeComponent();
            AddEvents();
        }

        private void AddEvents()
        {
            gameTimer.Tick += HandleTimerTick;
            this.KeyDown += HandleKeyDown;
            this.KeyUp += HandleKeyUp;
        }

        private void HandleKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            gameTimer.Start();
        }

        private void HandleKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
        }

        private void HandleTimerTick(object sender, EventArgs e)
        {
            _temp = ControlsHandler.Instance.GetVector();
            TryMove();
            this.AutoScrollPosition = moveableObject.Location;
            Console.WriteLine(moveableObject.Location.ToString());
            EndMove();
        }

        private void TryMove()
        {
            if(moveableObject.Location.X + _temp.x >= Constants.MIN_BOUND_X && moveableObject.Location.Y + _temp.y >= Constants.MIN_BOUND_Y)
            {
                moveableObject.Location = new Point(moveableObject.Location.X + _temp.x, moveableObject.Location.Y + _temp.y);
            }
        }

        private void EndMove()
        {
            if (!ControlsHandler.Instance.IsAnyKeyDown())
            {
                gameTimer.Stop();
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            // Do nothing
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new System.Drawing.Size(Constants.VIEW_SIZE_X, Constants.VIEW_SIZE_Y);
            this.MaximumSize = this.MinimumSize;
            moveableObject = pictureBox1;

        }
    }
}
