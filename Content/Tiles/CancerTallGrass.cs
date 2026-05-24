using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Metadata;

namespace TheCancerBiome.Content.Tiles
{
	public class CancerTallGrass : ModTile
	{
		public override void SetStaticDefaults() {
      
      Main.tileLavaDeath[Type] = true;
      Main.tileCut[Type] = true;
      Main.tileNoFail[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;

			TileID.Sets.DisableSmartCursor[Type] = true;
			TileID.Sets.ReplaceTileBreakUp[Type] = true;
			TileID.Sets.IgnoredInHouseScore[Type] = true;
			TileID.Sets.IgnoredByGrowingSaplings[Type] = true;
			TileID.Sets.SwaysInWindBasic[Type] = true;
      
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.CoordinateHeights = [16, 20];
			TileObjectData.newTile.AnchorValidTiles = [
				ModContent.TileType<CancerGrass>()
			];
			TileObjectData.addTile(Type);
			//TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);

			AddMapEntry(new Color(0xF1 - 16,0x76 - 16,0));
			HitSound = SoundID.Grass;
		}
		public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects) {
			if (i % 2 == 0) {
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
		}
		public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak) {
      Tile under = Framing.GetTileSafely(i, j+1);
      if(!under.HasTile || under.TileType != ModContent.TileType<CancerGrass>() || under.IsHalfBlock || under.TopSlope)
        WorldGen.KillTile(i, j);
      return true;
		}
		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY) {
      offsetY = 2;
		}
	}
}