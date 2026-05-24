using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ID;
using TheCancerBiome.Content.Dusts;
using TheCancerBiome.Content.Projectiles;

namespace TheCancerBiome.Content.Tiles
{
  public class OrangeIce : ModTile
  {
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileMerge[Type][TileID.SnowBlock] = true;
      TileID.Sets.Conversion.Ice[Type] = true;
      
			TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
			TileID.Sets.GeneralPlacementTiles[Type] = false;
			TileID.Sets.ChecksForMerge[Type] = true;
      
			AddMapEntry(new Color(0xF1,0x76,0x03));
      DustType = ModContent.DustType<CancerStoneDust>();
			HitSound = SoundID.Item50;
		}
  }
}