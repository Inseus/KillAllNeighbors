using KillAllNeighbors.Resources.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillAllNeighbors.Resources.Strategy.Implementation
{
    class Sniper : ShootAlgorithm
    {
        PictureBoxBuilder builder;
        public override Bullet Shoot(CreatorOfPictureBox creator)
        {
            creator = new CreatorOfPictureBox();
            // Kurėjas sukuria įrankį skirtą konstruoti zaidėjo pictur boxa
            builder = new SniperBulletBuilder();
            // Sukuriamas žaidėjo pictur boxas
            creator.ConstructMinimal(builder);
            return new Bullet(60, builder.GetResult());
        }
    }
}
