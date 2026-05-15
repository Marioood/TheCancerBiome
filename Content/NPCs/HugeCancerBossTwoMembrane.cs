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
	public class HugeCancerBossTwoMembrane : ModNPC
	{
    public int newCellCount = 2;
    
    public ref float AiStartMoveDir => ref NPC.ai[0];
    public int AiAnimTimer = 0;
    
		public override void SetStaticDefaults() {
			Main.npcFrameCount[Type] = 4;
      
			NPCID.Sets.DontDoHardmodeScaling[Type] = true;
			NPCID.Sets.CantTakeLunchMoney[Type] = true;
		}
    
    protected void PanSetup() {
			NPC.defense = 6;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 0;
			NPC.knockBackResist = 0.75f;
      NPC.aiStyle = -1;
    }
    
		public override void SetDefaults()
		{
      PanSetup();
      
			NPC.width = 32;
			NPC.height = 32;
			NPC.damage = 64;
      NPC.lifeMax = 200;
      NPC.scale = 8;
		}
    
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
      npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CancerBossItem>(), 3));
      npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TumorineOre>(), 3));
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
        
        Vector2 dirTo = NPC.DirectionTo(theTarget.Center);
        
        NPC.velocity += dirTo * 0.045f;
      }
      
      /*if(AiParentIdx > -1)
      {
        Entity theTarget = Main.npc[AiParentIdx];
        
        Vector2 dirTo = NPC.DirectionTo(theTarget.Center);
        
        NPC.velocity += dirTo * 0.05f;
      }*/
      
      if(NPC.collideX) {
        NPC.velocity.X *= -1.1f;
      }
      if(NPC.collideY) {
        NPC.velocity.Y *= -1.1f;
      }
      
      
      /*if(NPC.collideX || NPC.collideY) {
        NPC.noTileCollide = true;
      } else {
        NPC.noTileCollide = false;
      }*/
    }
    
    public override void FindFrame(int frameHeight) {
      NPC.frame.Y = (AiAnimTimer / 10 % 4) * frameHeight;
      AiAnimTimer++;
    }
    
    public override bool? CanFallThroughPlatforms() {
      return true;
    }
    
    public override void OnKill() {
      for(int i = 0; i < newCellCount; i++) {
        NPC minion = NPC.NewNPCDirect(
          NPC.GetSource_FromAI(),
          NPC.Center,
          ModContent.NPCType<ModerateCancerBossTwoMembrane>(),
          NPC.whoAmI,
          (float)Main.rand.NextDouble() * 6.28318530718f
        );
        /*if(minion.whoAmI != Main.maxNPCs) {
          //check for fail from spawn cap
          ModerateCancerBossTwoMembrane castedMinion = (ModerateCancerBossTwoMembrane)minion.ModNPC;
          castedMinion.AiParentIdx = AiParentIdx;
        }*/
      }
      
    }
	}
}
