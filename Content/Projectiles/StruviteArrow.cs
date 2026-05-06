using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using TheCancerBiome.Content.Dusts;
using Microsoft.Xna.Framework;
using System;

namespace TheCancerBiome.Content.Projectiles
{
  public class StruviteArrow : ModProjectile
  {
    public override void SetDefaults()
    {
      Projectile.width = 14;
      Projectile.height = 32;
      
      Projectile.friendly = true;
      Projectile.DamageType = DamageClass.Ranged;
    }
    
    public override void AI()
    {
      /*Player plr = Main.player[Projectile.owner];
      
      if(plr.active) {
        Projectile.velocity += Projectile.DirectionTo(new Vector2(plr.tileTargetX * 16, tileTargetY * 16)) * 0.25f;
      }*/
      
      //Projectile.velocity += Projectile.DirectionTo(Main.MouseWorld) * 0.25f;
      Projectile.velocity += Projectile.DirectionTo(Main.MouseWorld) * 0.25f;
      
      Projectile.rotation = (float)Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y);
      //Projectile.velocity.Y += 0.1f;
    }
    public override void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
      for(int i = 0; i < 32; i++)
      {
        Dust dust = Dust.NewDustDirect(Projectile.TopLeft, Projectile.width, Projectile.height, ModContent.DustType<PowerGloveDust>());
        dust.velocity *= ((float)Main.rand.NextDouble() * 0.25f) + 0.75f;
      }
    }
  }
}