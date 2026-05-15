using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ID;
using TheCancerBiome.Content.Dusts;

namespace TheCancerBiome.Content.Tiles
{
  public class Lumpstone : ModTile
  {
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileMergeDirt[Type] = true;
      TileID.Sets.Conversion.Stone[Type] = true;

			AddMapEntry(new Color(0x31,0x76,0xB2));
			HitSound = SoundID.Tink;
			MinPick = 65;
      DustType = ModContent.DustType<CancerStoneDust>();
		}
  }
}