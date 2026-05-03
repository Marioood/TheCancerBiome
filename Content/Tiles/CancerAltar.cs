using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace TheCancerBiome.Content.Tiles
{
	public class CancerAltar : ModTile
	{
		public override void SetStaticDefaults() {
			// Properties
			Main.tileNoAttach[Type] = true;
			Main.tileFrameImportant[Type] = true;
      Main.tileHammer[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			
			AdjTiles = [TileID.DemonAltar];

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.CoordinateHeights = [16, 18];
			TileObjectData.addTile(Type);

			// Etc
			AddMapEntry(new Color(0, 255, 255));
		}
	}
}