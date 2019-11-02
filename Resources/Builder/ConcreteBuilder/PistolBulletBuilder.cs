using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace KillAllNeighbors.Resources.Builder
{
    class PistolBulletBuilder : PictureBoxBuilder
    {
        public PistolBulletBuilder(PictureBox boxx) : base(boxx)
        {
        }
        public override PictureBox GetResult()
        {
            return box;
        }

        public override PictureBoxBuilder BuildName()
        {
            box.Name = "bullet";
            return this;
        }


        public override PictureBoxBuilder BuildPictureColor()
        {
            box.BackColor = System.Drawing.Color.Black; // set the colour white for the bullet
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
            box.Size= new Size(5, 5);
            return this;
        }

        public override PictureBoxBuilder BuildLocation()
        {
            throw new NotImplementedException();
        }
    }
}
