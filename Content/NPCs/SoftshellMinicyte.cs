using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using TheCancerBiome.Content.Items;
using TheCancerBiome.Content.Items.Placeable;
using System;
using Microsoft.Xna.Framework;

namespace TheCancerBiome.Content.NPCs
{
  public class SoftshellMinicyte : ModNPC
	{
    public ref float AiStartMoveDir => ref NPC.ai[0];
    
		public override void SetStaticDefaults() {
			Main.npcFrameCount[Type] = 1;
		}

		public override void SetDefaults() {
			NPC.width = 33;
			NPC.height = 57;
			NPC.damage = 16;
			NPC.defense = 6;
			NPC.lifeMax = 20;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 0;
			NPC.knockBackResist = 0.8f;
			NPC.aiStyle = NPCAIStyleID.DemonEye;
		}

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
      npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Cytoplasm>(), 2));
      npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TumorineOre>(), 2));
    }

		public override void AI() {
      if(AiStartMoveDir > 0) {
        NPC.velocity = new Vector2((float)Math.Cos(AiStartMoveDir), (float)Math.Sin(AiStartMoveDir)) * 4;
        AiStartMoveDir = 0;
      }
      
      NPC.TargetClosest(true);
      
      if(NPC.HasValidTarget)
      {
        Player thePlayer = Main.player[NPC.target];
        float angTo = NPC.AngleTo(thePlayer.Center);
        NPC.rotation = angTo - (float)Math.PI / 2;
      }
		}
	}
}