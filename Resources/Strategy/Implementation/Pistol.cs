using KillAllNeighbors.Resources.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillAllNeighbors.Resources.Strategy.Implementation
{
    class Pistol : ShootAlgorithm
    {
        PictureBoxBuilder builder;
        public Pistol(CreatorOfPictureBox _creator) : base(_creator) { }
        public override Bullet Shoot()
        {
            // Kurėjas sukuria įrankį skirtą konstruoti zaidėjo pictur boxa
            builder = new PistolBulletBuilder(new PictureBox());
            // Sukuriamas žaidėjo pictur boxas
            var box = creator.ConstructMinimal(builder);
            return new Bullet(60, box);
        }
    }
}
