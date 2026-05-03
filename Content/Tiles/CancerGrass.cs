using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace TheCancerBiome.Content.Tiles
{
  public class CancerGrass : ModTile
  {
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileBlendAll[Type] = true;
			Main.tileMergeDirt[Type] = true;
      
			TileID.Sets.Grass[Type] = true;
			TileID.Sets.NeedsGrassFraming[Type] = true;
			TileID.Sets.NeedsGrassFramingDirt[Type] = TileID.Dirt;
			TileID.Sets.Conversion.Grass[Type] = true;

      //SetModTree(new )
			AddMapEntry(new Color(0xF1,0x76,0x03));
			//drop = ItemID.DirtBlock;
		}
    
		public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			if (!fail) //Change self into dirt
			{
				fail = true;
				Framing.GetTileSafely(i, j).ResetToType(TileID.Dirt);
			}
		}
  }
}