using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace TheCancerBiome.Content.NPCs
{ 
	public class ModerateCancerBossTwoMembrane : HugeCancerBossTwoMembrane
	{
    public ref float AiTeleTimer => ref NPC.ai[1];
    
		public override void SetDefaults()
		{
      PanSetup();
      
			NPC.width = 96;
			NPC.height = 96;
			NPC.damage = 50;
      NPC.lifeMax = 120;
		}
    
    public override void AI()
    {
      if(AiStartMoveDir > 0) {
        NPC.velocity = new Vector2((float)Math.Cos(AiStartMoveDir), (float)Math.Sin(AiStartMoveDir)) * 4;
        AiStartMoveDir = 0;
      }
      
      NPC.GravityMultiplier *= 0;
      
      NPC.TargetClosest(true);
      
      if(NPC.HasValidTarget)
      {
        Entity theTarget = Main.player[NPC.target];
        
        if(AiTeleTimer > 4 * 60) {
          Vector2 dirTo = NPC.DirectionTo(theTarget.Center);
          NPC.velocity += dirTo * 12;
          AiTeleTimer = 0;
        }
        AiTeleTimer++;
      }
      NPC.velocity *= 0.99f;
      
      if(NPC.collideX) {
        NPC.velocity.X *= -1.1f;
      }
      if(NPC.collideY) {
        NPC.velocity.Y *= -1.1f;
      }
    }
    
    public override void OnKill() {
      for(int i = 0; i < newCellCount; i++) {
        NPC minion = NPC.NewNPCDirect(
          NPC.GetSource_FromAI(),
          NPC.Center,
          ModContent.NPCType<SmallCancerBossTwoMembrane>(),
          NPC.whoAmI,
          (float)Main.rand.NextDouble() * 6.28318530718f
        );
        /*if(minion.whoAmI != Main.maxNPCs) {
          //check for fail from spawn cap
          SmallCancerBossTwoMembrane castedMinion = (SmallCancerBossTwoMembrane)minion.ModNPC;
          castedMinion.AiParentIdx = AiParentIdx;
        }*/
      }
      
    }
	}
}
