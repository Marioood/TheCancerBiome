using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ID;
using TheCancerBiome.Content.Dusts;
using TheCancerBiome.Content.Projectiles;

namespace TheCancerBiome.Content.Tiles
{
  public class Lumpsand : ModTile
  {
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileMergeDirt[Type] = true;
      
      Main.tileSand[Type] = true;
			TileID.Sets.Conversion.Sand[Type] = true; // Allows Clentaminator solutions to convert this tile to their respective Sand tiles.
			TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true; // Allows Sandshark enemies to "swim" in this sand.
			TileID.Sets.CanBeDugByShovel[Type] = true;
			TileID.Sets.Falling[Type] = true;
			TileID.Sets.Suffocate[Type] = true;
			TileID.Sets.FallingBlockProjectile[Type] = new TileID.Sets.FallingBlockProjectileInfo(ModContent.ProjectileType<LumpsandBallFallingProjectile>(), 10); // Tells which falling projectile to spawn when the tile should fall.

			TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
			TileID.Sets.GeneralPlacementTiles[Type] = false;
			TileID.Sets.ChecksForMerge[Type] = true;
      
			AddMapEntry(new Color(0x31,0x76,0xB2));
      DustType = ModContent.DustType<CancerStoneDust>();
      MineResist = 0.5f; 
		}

		public override bool HasWalkDust() {
			return Main.rand.NextBool(3);
		}

		public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color) {
			dustType = ModContent.DustType<CancerStoneDust>();
		}
  }
}