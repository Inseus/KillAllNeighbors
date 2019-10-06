using KillAllNeighbors.Resources.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Strategy.Implementation
{
    class Machinegun : ShootAlgorithm
    {
        PictureBoxBuilder builder;
        public override Bullet Shoot(CreatorOfPictureBox creator)
        {

            // Sukuriama kurėją
            creator = new CreatorOfPictureBox();
            // Kurėjas sukuria įrankį skirtą konstruoti zaidėjo pictur boxa
            builder = new BulletBuilder();
            // Sukuriamas žaidėjo pictur boxas
            creator.ConstructMinimal(builder);
            return new Bullet(60, builder.GetResult());
        }
    }
}
