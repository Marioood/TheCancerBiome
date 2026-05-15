using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using TheCancerBiome.Content.Dusts;
using Microsoft.Xna.Framework;
using System;

namespace TheCancerBiome.Content.Projectiles
{
  public class PowerGloveHand : ModProjectile
  {
    public override void SetDefaults()
    {
      Projectile.width = 26;
      Projectile.height = 26;
      
      Projectile.friendly = true;
      Projectile.DamageType = DamageClass.Magic;
    }
    
    public override void AI()
    {
      Projectile.velocity *= 0.99f;
      
      if(Projectile.owner == Main.myPlayer) {
        Projectile.ai[0] = Main.MouseWorld.X;
        Projectile.ai[1] = Main.MouseWorld.Y;
      }
      Projectile.velocity += Projectile.DirectionTo(new Vector2(Projectile.ai[0], Projectile.ai[1])) * 0.25f;
      
      Projectile.rotation = (float)Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y);
      
      Dust dust = Dust.NewDustDirect(Projectile.TopLeft, Projectile.width, Projectile.height, ModContent.DustType<PowerGloveDust>());
      dust.velocity *= 0.5f;
    }
    
    public override Color? GetAlpha(Color lightColor) {
      //float col = 1 - (Projectile.timeLeft / (float)maxTime);
      return new Color(1f, 1f, 1f, 0f);
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