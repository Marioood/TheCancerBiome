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
	public class TumorineBar : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileShine[Type] = 1100;
			Main.tileSolid[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileFrameImportant[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(0xF1,0x76,0x03), Language.GetText("MapObject.MetalBar"));
		}

		public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak) {
			if(!WorldGen.SolidTileAllowBottomSlope(i, j + 1)) {
				WorldGen.KillTile(i, j);
			}
			return true;
		}
	}
}