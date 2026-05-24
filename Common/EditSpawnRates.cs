using Terraria.ModLoader;
using Terraria;
using TheCancerBiome.Common.Systems;
using TheCancerBiome.Content.NPCs;
using System;
using System.Collections.Generic;
using TheCancerBiome.Content.Biomes;

namespace TheCancerBiome.Common {
  
  public class EditSpawnRates : GlobalNPC {
    
		public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo) {
      if(spawnInfo.Player.InModBiome<CancerSurfaceBiome>() || spawnInfo.Player.InModBiome<CancerUndergroundBiome>()) {
        //TODO: one or more npcs that spawn after downing the evil biome boss
        
        pool.Clear();
        
        pool.Add(ModContent.NPCType<FatHead>(), 0.5f);
        pool.Add(ModContent.NPCType<Immunocyte>(), 0.4f);
        pool.Add(ModContent.NPCType<StickyBuddy>(), 0.5f);
      }
      
    }
  }
}