using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Builder
{
    public class CreatorOfPictureBox
    {
        public void Construct(PictureBoxBuilder builder)
        {
            builder.BuildLocation();
            builder.BuildName();
            builder.BuildPictureColor();
            builder.BuildPictureImage();
            builder.BuildPictureSize();
        }
        public void ConstructMinimal(PictureBoxBuilder builder)
        {
            builder.BuildName();
            builder.BuildPictureColor();
            builder.BuildPictureSize();
        }
    }
}
