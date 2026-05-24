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
      MineResist = 0.1f;
		}
    
		public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			if (!fail) //Change self into dirt
			{
				fail = true;
				Framing.GetTileSafely(i, j).ResetToType(TileID.Dirt);
			}
		}
    
    public override void RandomUpdate(int i, int j) {
      Tile tile = Framing.GetTileSafely(i, j);
      Tile above = Framing.GetTileSafely(i, j-1);
      
			if (!above.HasTile)
			{
        if(WorldGen.genRand.NextBool(8)) {
          if(!tile.BottomSlope && !tile.TopSlope && !tile.IsHalfBlock)
          {
            above.ResetToType((ushort)ModContent.TileType<CancerTallGrass>());
            above.HasTile = true;
            above.TileFrameY = 0;
            above.TileFrameX = (short)(WorldGen.genRand.Next(8) * 18);
            WorldGen.SquareTileFrame(i, j + 1, true);
            if (Main.netMode == NetmodeID.Server)
              NetMessage.SendTileSquare(-1, i, j - 1, 3, TileChangeType.None);
          }
        } else if(WorldGen.genRand.NextBool(24)) {
          if(!tile.BottomSlope && !tile.TopSlope && !tile.IsHalfBlock)
          {
            above.ResetToType(TileID.ImmatureHerbs);
            above.HasTile = true;
            above.TileFrameY = 52;
            above.TileFrameX = 0;
            WorldGen.SquareTileFrame(i, j + 1, true);
            if (Main.netMode == NetmodeID.Server)
              NetMessage.SendTileSquare(-1, i, j - 1, 3, TileChangeType.None);
          }
        }
			}
    }
  }
}