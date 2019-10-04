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
        private PictureBox box = new PictureBox();
        public override PictureBox GetResult()
        {
            return box;
        }

        public override void BuildLocation()
        {
            box.Location = new Point(0, 0);
        }

        public override void BuildName()
        {
            box.Name = "pictureBoxPlayer";
        }


        public override void BuildPictureColor()
        {
            box.BackColor = System.Drawing.Color.Transparent;
        }

        public override void BuildPictureImage()
        {
            
            box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            box.Image = global::KillAllNeighbors.Properties.Resources.cirle;
        }

        public override void BuildPictureSize()
        {
            box.Size= new Size(35, 35);
        }
    }
}
