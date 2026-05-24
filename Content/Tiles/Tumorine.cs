using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using TheCancerBiome.Content.Dusts;
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
			HitSound = SoundID.Tink;

			LocalizedText name = CreateMapEntryName();
			AddMapEntry(new Color(0xF1,0x76,0x03), name);
      DustType = ModContent.DustType<TumorineDust>();
		}
  }
}