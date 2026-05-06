using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using TheCancerBiome.Content.Items;

namespace TheCancerBiome.Content.Tiles
{
	public class SwollenNucleus : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileNoAttach[Type] = true;
			Main.tileFrameImportant[Type] = true;
      Main.tileHammer[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

      TileObjectData.newTile.Width = 2;
      TileObjectData.newTile.Height = 2;
      TileObjectData.newTile.CoordinateWidth = 16;
      TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = [16, 16];
      TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(0, 255, 255));
			AnimationFrameHeight = 36;
		}
    
    public override void KillMultiTile(int i, int j, int frameX, int frameY) {
      //look in WorldGen.cs for this code
			if (!WorldGen.gen && Main.netMode != NetmodeID.MultiplayerClient) {
        WorldGen.shadowOrbCount++;
        WorldGen.shadowOrbSmashed = true;
      }
      
		}
    
    public override IEnumerable<Item> GetItemDrops(int i, int j) {
      int choice = Main.rand.Next(2);
      
      switch(choice) {
        case 0:
          return new[] {new Item(ModContent.ItemType<PowerGlove>())};
        case 1:
          return new[] {new Item(ModContent.ItemType<HandCannon>()), new Item(ItemID.MusketBall, 100)};
      }
      
      return null;
    }
    
		public override void AnimateTile(ref int frame, ref int frameCounter) {
			frame = Main.tileFrame[TileID.ShadowOrbs];
		}
    
	}
}