using KillAllNeighbors.Resources.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources.Decorator
{
    public class Enemy : Unit
    {
        PictureBox movableObject;
        public CreatorOfPictureBox creator;
        public Enemy(CreatorOfPictureBox creator,long id ) : base()
        {
            this.creator = creator;
            createPictureBox();
            SetId(id);
        }

        public void setDirectionUp()
        {
            facing = "up";
        }
        public void setDirectionDown()
        {
            facing = "down";
        }
        public void setDirectionLeft()
        {
            facing = "left";
        }
        public void setDirectioRight()
        {
            facing = "right";
        }
        public string getDirection()
        {
            return facing;
        }

        public void createPictureBox()
        {
            PictureBoxBuilder builder = new EnemyBoxBuilder(new PictureBox());
            // Sukuriamas žaidėjo pictur boxas
            movableObject = creator.Construct(builder);
        }
        public PictureBox getMovableObject()
        {
            return movableObject;
        }
        public void addToForm(Form1 form)
        {
            form.Controls.Add(movableObject);
        }
    }
}
