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
			Main.tileLighted[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileFrameImportant[Type] = true;
      Main.tileHammer[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			
			AdjTiles = [TileID.DemonAltar];

			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.CoordinateHeights = [16, 18];
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(0x00,0x71,0xDD));
		}
    
    public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem) {
      //if(!noItem) fail = true;
      /*if (tile.type == (ushort) 26 && (sItem.hammer < 80 || !Main.hardMode))
      {
        damageAmount = 0;
        this.Hurt(PlayerDeathReason.ByOther(4), this.statLife / 2, -this.direction, false, false, false, -1);
      }*/
    }
    
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
      float pulse = Main.rand.Next(28, 42) * 0.005f;
      r = 0.1f/8 + pulse;
      g = 0.7f/8 + pulse;
      b = 0.9f/8 + pulse;
		}
	}
}