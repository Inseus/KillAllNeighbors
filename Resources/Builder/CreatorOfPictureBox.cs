using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources.Builder
{
    public class CreatorOfPictureBox
    {
        public PictureBox Construct(PictureBoxBuilder builder)
        {
            return builder.BuildLocation()
                .BuildName()
                .BuildName()
                .BuildPictureColor()
                .BuildPictureImage()
                .BuildPictureSize().GetResult();

        }
        public PictureBox ConstructMinimal(PictureBoxBuilder builder)
        {
            return builder.BuildName()
            .BuildPictureColor()
            .BuildPictureSize().GetResult();
        }
    }
}
