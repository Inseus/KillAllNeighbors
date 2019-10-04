using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Builder
{
    class CreatorOfPictureBox
    {
        public void Construct(PictureBoxBuilder builder)
        {
            builder.BuildLocation();
            builder.BuildName();
            builder.BuildPictureColor();
            builder.BuildPictureImage();
            builder.BuildPictureSize();
        }
    }
}
