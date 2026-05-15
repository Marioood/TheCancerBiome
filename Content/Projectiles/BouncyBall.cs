using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using TheCancerBiome.Content.Dusts;
using Microsoft.Xna.Framework;
using System;

namespace TheCancerBiome.Content.Projectiles
{
  public class BouncyBall : ModProjectile
  {
    private SoundStyle Bounce = new SoundStyle("TheCancerBiome/Assets/Sounds/BouncyBall") {
      Volume = 0.25f,
      PitchVariance = 0.5f,
      MaxInstances = 8
    };
    private SoundStyle BigBounce = new SoundStyle("TheCancerBiome/Assets/Sounds/BigBoing") {
      Volume = 0.5f,
      PitchVariance = 0.5f,
      MaxInstances = 8
    };
    
    public ref float AiHits => ref Projectile.ai[0];
    
    public override void SetDefaults()
    {
      Projectile.width = 24;
      Projectile.height = 24;
      
      Projectile.friendly = true;
      Projectile.DamageType = DamageClass.Ranged;
    }
    
    public override void AI()
    {
      /*if(Projectile.collideX) {
        Projectile.velocity.X *= -0.99;
        SoundEngine.PlaySound(Bounce, Projectile.Center);
      }
      if(Projectile.collideY) {
        Projectile.velocity.Y *= -0.99;
        SoundEngine.PlaySound(Bounce, Projectile.Center);
      }*/
      Projectile.velocity.Y += 0.25f;
    }
		public override bool OnTileCollide(Vector2 oldVelocity) {
      //Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
      AiHits++;
      
      if(AiHits >= 5) {
        SoundEngine.PlaySound(SoundID.Item54, Projectile.Center);
        Projectile.Kill();
      } else {
        SoundEngine.PlaySound(Bounce, Projectile.Center);
      }

      if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) {
        Projectile.velocity.X = oldVelocity.X * -0.95f;
      }

      if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) {
        Projectile.velocity.Y = oldVelocity.Y * -0.95f;
      }
      return false;
    }
    
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if(target.life < 0) {
        SoundEngine.PlaySound(BigBounce, Projectile.Center);
      }
    }
    
    public override bool PreKill(int timeLeft) {
      return false;
    }
    
    public override void OnKill(int timeLeft) {
      for(int i = 0; i < 32; i++)
      {
        Dust dust = Dust.NewDustDirect(Projectile.TopLeft, Projectile.width, Projectile.height, ModContent.DustType<CancerStoneDust>());
        dust.velocity *= ((float)Main.rand.NextDouble() * 0.25f) + 0.75f;
      }
    }
    
  }
}