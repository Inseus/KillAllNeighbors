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
        public abstract void BuildName();
        public abstract void BuildPictureImage();

        public abstract void BuildPictureSize();

        public abstract void BuildLocation();

        public abstract void BuildPictureColor();
        public abstract PictureBox GetResult();
    }
}
