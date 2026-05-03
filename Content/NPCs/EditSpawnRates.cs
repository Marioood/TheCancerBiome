using Terraria.ModLoader;
using Terraria;
using TheCancerBiome.Common.Systems;
using System;
using System.Collections.Generic;

namespace TheCancerBiome.Content.NPCs {
  
  public class EditSpawnRates : GlobalNPC {
    
		public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo) {
      //Player plr = spawnInfo.player;
      
      if(ModContent.GetInstance<CancerBiomeTileCount>().cancerBlockCount >= 40) {
        //TODO: one or more npcs that spawn after downing the evil biome boss
        
        pool.Clear();
        
        pool.Add(ModContent.NPCType<FatHead>(), 0.5f);
        pool.Add(ModContent.NPCType<Immunocyte>(), 0.4f);
        pool.Add(ModContent.NPCType<StickyBuddy>(), 0.5f);
      }
      
    }
  }
}