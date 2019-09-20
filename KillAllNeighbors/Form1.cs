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
        Timer gameTimer = new Timer { Interval = 80};
        Vector2 _temp = new Vector2();
        public Form1()
        {
            InitializeComponent();
            gameTimer.Tick += HandleTimerTick;
            this.KeyDown += HandleKeyDown;
            this.KeyUp += HandleKeyUp;
        }

        private void HandleKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            _temp = ControlsHandler.Instance.GetVector();
            gameTimer.Start();
        }

        private void HandleKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            
        }

        private void HandleTimerTick(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X + _temp.x, pictureBox1.Location.Y + _temp.y);
            EndMove();
        }

        private void EndMove()
        {
            if (!ControlsHandler.Instance.IsAnyKeyDown())
            {
                gameTimer.Stop();
            }
            else
            {

            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            // Do nothing
        }
    }
}
