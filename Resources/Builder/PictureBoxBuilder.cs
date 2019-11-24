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
        public virtual bool doBuildName()
        {
            return true;
        }
        public virtual bool doBuildPictureImage()
        {
            return true;
        }
        public virtual bool doBuildPictureSize()
        {
            return true;
        }
        public virtual bool doBuildLocation()
        {
            return true;
        }
        public virtual bool doBuildPictureColor()
        {
            return true;
        }
        public abstract void BuildName();
        public abstract void BuildPictureImage();

        public abstract void BuildPictureSize();

        public abstract void BuildLocation();

        public abstract void BuildPictureColor();
        public abstract PictureBox GetResult();
        public void TemplateMethod()
        {
            if(doBuildName())
            BuildName();
            if(doBuildPictureImage())
            BuildPictureImage();
            if (doBuildPictureSize())
                BuildPictureSize();
            if (doBuildLocation())
                BuildLocation();
            if (doBuildPictureColor())
                BuildPictureColor();
        }
    }
}
