using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Chat;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using TheCancerBiome.Content.Items;
using TheCancerBiome.Content.NPCs;
using System;

namespace TheCancerBiome.Content.Tiles
{
	public class SwollenNucleus : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileLighted[Type] = true;
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

			AddMapEntry(new Color(0x00,0x71,0xDD));
			AnimationFrameHeight = 36;
		}
    
    public override void KillMultiTile(int i, int j, int frameX, int frameY) {
      //look in WorldGen.cs for this code
			if (!WorldGen.gen && Main.netMode != NetmodeID.MultiplayerClient) {
        int bossType = ModContent.NPCType<PrimaryClone>();
        WorldGen.shadowOrbCount++;
        WorldGen.shadowOrbSmashed = true;
        
        if(WorldGen.shadowOrbCount >= 3)
        {
          if (!NPC.AnyNPCs(bossType))
          {
            WorldGen.shadowOrbCount = 0;
            float num3 = (float) (i * 16);
            float num4 = (float) (j * 16);
            float num5 = -1f;
            int plr = 0;
            for (int index = 0; index < (int) byte.MaxValue; ++index)
            {
              float num6 = Math.Abs(Main.player[index].position.X - num3) + Math.Abs(Main.player[index].position.Y - num4);
              if ((double) num6 < (double) num5 || (double) num5 == -1.0)
              {
                plr = index;
                num5 = num6;
              }
            }
            NPC.SpawnOnPlayer(plr, bossType);
          }
        }
        else
        {
          LocalizedText localizedText = Lang.misc[10];
          if (WorldGen.shadowOrbCount == 2)
            localizedText = Lang.misc[11];
          switch (Main.netMode)
          {
            case 0:
              Main.NewText(localizedText.ToString(), (byte) 50, byte.MaxValue, (byte) 130);
              break;
            case 2:
              ChatHelper.BroadcastChatMessage(NetworkText.FromKey(localizedText.Key), new Color(50, (int) byte.MaxValue, 130), -1);
              break;
          }
        }
        
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
    
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
      float pulse = Main.rand.Next(28, 42) * 0.005f;
      r = 0.1f/8 + pulse;
      g = 0.7f/8 + pulse;
      b = 0.9f/8 + pulse;
		}
	}
}