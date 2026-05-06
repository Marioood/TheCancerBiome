using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using TheCancerBiome.Content.Dusts;
using Microsoft.Xna.Framework;
using System;

namespace TheCancerBiome.Content.Projectiles
{
  public class CancerBossProjectile : ModProjectile
  {
    public override void SetDefaults()
    {
      Projectile.width = 12;
      Projectile.height = 26;
      Projectile.timeLeft = 60 * 60;
      Projectile.hostile = true;
    }
    
    public override void AI()
    {
      Projectile.velocity.Y += 0.075f;
      Projectile.rotation = (float)Math.Atan2(Projectile.velocity.X, -Projectile.velocity.Y);
    }
  }
}