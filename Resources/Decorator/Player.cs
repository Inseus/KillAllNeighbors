using KillAllNeighbors.Resources.Builder;
using KillAllNeighbors.Resources.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources
{
    public class Player : Unit
    {
        PictureBox movableObject;
        public CreatorOfPictureBox creator;
        public Player(CreatorOfPictureBox creator) :base()
        {
            facing = "down";
            this.creator = creator;
            createPictureBox();
        }
        public void setCordinatesFromPictureBoxToPlayer()
        {
            PosX = movableObject.Location.X;
            PosY = movableObject.Location.Y;
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
           PictureBoxBuilder builder = new PlayerBoxBuilder(new PictureBox());
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
