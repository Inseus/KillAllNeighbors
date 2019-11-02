using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources.Builder
{
    public abstract class PictureBoxBuilder
    {
        protected PictureBox box;
        public PictureBoxBuilder( PictureBox m)
        {
            box = m;
        }
        public abstract PictureBoxBuilder BuildName();
        public abstract PictureBoxBuilder BuildPictureImage();

        public abstract PictureBoxBuilder BuildPictureSize();

        public abstract PictureBoxBuilder BuildLocation();

        public abstract PictureBoxBuilder BuildPictureColor();
        public abstract PictureBox GetResult();
    }
}
