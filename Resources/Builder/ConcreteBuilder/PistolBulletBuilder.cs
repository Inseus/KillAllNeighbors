﻿using System;
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

        public override void BuildName()
        {
            box.Name = "bullet";
        }


        public override void BuildPictureColor()
        {
            box.BackColor = System.Drawing.Color.Black; // set the colour white for the bullet

        }

        public override void BuildPictureImage()
        {
            
            box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            box.Image = global::KillAllNeighbors.Properties.Resources.cirle;
        }

        public override void BuildPictureSize()
        {
            box.Size= new Size(5, 5);
        }

        public override void BuildLocation()
        {
            throw new NotImplementedException();
        }
        public override bool doBuildPictureImage()
        {
            return false;
        }

        public override bool doBuildLocation()
        {
            return false;
        }
    }
}
