using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TheCancerBiome.Content.Dusts
{
  class PowerGloveDust : ModDust
  {
    public override void OnSpawn(Dust dust)
    {
      dust.noLight = true;
      dust.fadeIn = (float)((Main.rand.NextDouble()) - 0.5f) / 2.0f;
      dust.alpha = (byte)0;
    }
    
    public override Color? GetAlpha(Dust dust, Color lightColor) {
      return new Color(1f, 1f, 1f, 0f);
    }
    
    public override bool Update(Dust dust)
    {
      dust.position += dust.velocity;
      dust.scale *= 0.99f;
      dust.rotation += dust.fadeIn;
      
      if(dust.scale < 0.25f)
      {
        dust.active = false;
      }
      return false;
    }
  }
}