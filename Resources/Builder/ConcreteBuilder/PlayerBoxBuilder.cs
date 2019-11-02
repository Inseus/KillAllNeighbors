using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace KillAllNeighbors.Resources.Builder
{
    class PlayerBoxBuilder : PictureBoxBuilder
    {
        public PlayerBoxBuilder(PictureBox boxx) : base(boxx)
        {
        }
        public override PictureBox GetResult()
        {
            return box;
        }

        public override PictureBoxBuilder BuildLocation()
        {
            box.Location = new Point(0, 0);
            return this;
        }

        public override PictureBoxBuilder BuildName()
        {
            box.Name = "player";
            return this;
        }


        public override PictureBoxBuilder BuildPictureColor()
        {
            box.BackColor = System.Drawing.Color.Transparent;
            return this;
        }

        public override PictureBoxBuilder BuildPictureImage()
        {
            
            box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            box.Image = global::KillAllNeighbors.Properties.Resources.cirle;
            return this;
        }

        public override PictureBoxBuilder BuildPictureSize()
        {
            box.Size= new Size(35, 35);
            return this;
        }
    }
}
