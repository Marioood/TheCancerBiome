using Terraria.ModLoader;
using Terraria;
using TheCancerBiome.Common.Systems;
using TheCancerBiome.Content.NPCs;
using TheCancerBiome.Content.Items;
using TheCancerBiome.Content.Items.Placeable;
using System;
using System.Collections.Generic;
using TheCancerBiome.Content.Biomes;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;

namespace TheCancerBiome.Common {
  
  public class EditVanillaLoot : GlobalNPC {
    //see Terraria/GameContent/ItemDropRules/ItemDropDatabase.cs
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
      if(npc.type == NPCID.EyeofCthulhu) {
        npcLoot.RemoveWhere(
          rule => rule is ItemDropWithConditionRule drop 
          && (drop.itemId == ItemID.DemoniteOre
          || drop.itemId == ItemID.CorruptSeeds
          || drop.itemId == ItemID.CrimtaneOre
          || drop.itemId == ItemID.CrimsonSeeds
          || drop.itemId == ItemID.UnholyArrow)
        );
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TumorineOre>(), 1, 30, 90));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CancerSeeds>(), 1, 1, 3));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StruviteArrow>(), 1, 20, 50));
      }
    }
    
  }
}