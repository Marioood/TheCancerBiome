using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using TheCancerBiome.Content.Items;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;

namespace TheCancerBiome.Content.NPCs
{ 
	public class SmallCancerBossTwoMembrane : HugeCancerBossTwoMembrane
	{
		public override void SetDefaults()
		{
      PanSetup();
      
			NPC.width = 32;
			NPC.height = 32;
			NPC.damage = 25;
      NPC.lifeMax = 30;
      NPC.scale = 2;
		}
    
    public override void OnKill() {
      for(int i = 0; i < newCellCount; i++) {
        NPC minion = NPC.NewNPCDirect(
          NPC.GetSource_FromAI(),
          NPC.Center,
          ModContent.NPCType<TinyCancerBossTwoMembrane>(),
          NPC.whoAmI,
          (float)Main.rand.NextDouble() * 6.28318530718f
        );
        /*if(minion.whoAmI != Main.maxNPCs) {
          //check for fail from spawn cap
          TinyCancerBossTwoMembrane castedMinion = (TinyCancerBossTwoMembrane)minion.ModNPC;
          castedMinion.AiParentIdx = AiParentIdx;
        }*/
      }
      
    }
	}
}
