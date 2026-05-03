using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.ItemDropRules;
using TheCancerBiome.Content.Items;
using Microsoft.Xna.Framework;
using System;

namespace TheCancerBiome.Content.NPCs
{ 
	public class StickyBuddy : ModNPC
	{
    private float maxWaitTimer = 3 * 20;
    
    private ref float AiWaitTimer => ref NPC.ai[0];
    
		public override void SetStaticDefaults() {
			Main.npcFrameCount[Type] = 4;
		}
    
		public override void SetDefaults()
		{
			NPC.width = 32;
			NPC.height = 28;
			NPC.defense = 8;
			NPC.lifeMax = 40;
			NPC.damage = 22;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 100f;
			NPC.knockBackResist = 0.75f;
      NPC.aiStyle = -1;
		}
    
    public override void AI()
    {
      //https://docs.tmodloader.net/docs/stable/class_n_p_c.html#a24627ed4a99fbc0555b1be4717a9335e
      
      NPC.TargetClosest(true);
      float dir = Main.rand.Next(2) == 0 ? 1 : -1;
      
      if(NPC.HasValidTarget) {
        Player thePlayer = Main.player[NPC.target];
        dir = NPC.Center.X < thePlayer.Center.X ? 1 : -1;
      }
        
      if(AiWaitTimer > maxWaitTimer) {
        AiWaitTimer = 0;
        NPC.velocity.Y = -0.65f * 16;
        NPC.velocity.X = 0.5f * 16 * dir;
        
      } else if(NPC.velocity.Length() < 0.1f && NPC.collideY) {
        AiWaitTimer++;
      }
      
      NPC.velocity *= NPC.collideY ? 0.8f : 0.99f;
      if(NPC.collideX) {
        NPC.velocity.X *= -1f;
      }
    }
    
    public override void FindFrame(int frameHeight) {
      int dividend = AiWaitTimer > maxWaitTimer - 0.5f * 20 ? 4 : 8;
      NPC.frame.Y = ((int)AiWaitTimer / dividend % 2) * frameHeight;
    }
    
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
      npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LipoidalClump>(), 3, 1));
      npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 3, 1));
    }
	}
}
