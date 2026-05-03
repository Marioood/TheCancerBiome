using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheCancerBiome.Content.Walls
{
	public class TumorineWall : ModWall
	{
		public override void SetStaticDefaults() {
			Main.wallBlend[Type] = WallID.Stone;
      
			AddMapEntry(new Color(0xF1/2,0x76/2,0));
		}
	}
}