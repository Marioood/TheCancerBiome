using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace TheCancerBiome.Content.Tiles
{
  public class Tumorine : ModTile
  {
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileMergeDirt[Type] = true;

			AddMapEntry(new Color(0xF1,0x76,0x03));
			HitSound = SoundID.Tink;
		}
  }
}