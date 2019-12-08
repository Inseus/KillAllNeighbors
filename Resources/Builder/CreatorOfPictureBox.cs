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
            // everything
            builder.TemplateMethod();
            return builder.GetResult();
        }
        public PictureBox ConstructMinimal(PictureBoxBuilder builder)
        {
            // name color size
            builder.TemplateMethod();
            return builder.GetResult();
        }
        public PictureBox ConstructEnemyBullets(PictureBoxBuilder builder)
        {
            // name color size
            builder.TemplateMethod();
            return builder.GetResult();
        }
    }
}
