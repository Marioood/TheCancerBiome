using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ID;
using TheCancerBiome.Content.Dusts;

namespace TheCancerBiome.Content.Tiles
{
  public class Struvite : ModTile
  {
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;

			LocalizedText name = CreateMapEntryName();
			AddMapEntry(new Color(0xE2,0xA9,0x80), name);

			//DustType = DustID.Platinum;
			HitSound = SoundID.Tink;
      DustType = ModContent.DustType<TumorineDust>();
		}
  }
}