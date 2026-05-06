using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using TheCancerBiome.Content.Items;
using TheCancerBiome.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;

namespace TheCancerBiome.Content.NPCs
{ 
  [AutoloadBossHead]
	public class CancerBossTwo : ModNPC
	{
    public ref float AiState => ref NPC.ai[0];
    public ref float AiTeleTimer => ref NPC.ai[1];
    public ref float AiTurnMult => ref NPC.ai[2];
    
    float StateSetup = 0;
    float StateWait = 1;
    float StateFire = 2;
    
		public override void SetStaticDefaults() {
			Main.npcFrameCount[Type] = 1;
			NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
      NPCID.Sets.MPAllowedEnemies[Type] = true;
		}
    
		public override void SetDefaults()
		{
			NPC.width = 64;
			NPC.height = 64;
			NPC.damage = 50;
			NPC.defense = 6;
			NPC.lifeMax = 2500;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath23;
			NPC.value = 5 * 100 * 100;
			NPC.knockBackResist = 0;
      NPC.aiStyle = -1;
			NPC.boss = true;
			NPC.npcSlots = 10; // Take up open spawn slots, preventing random NPCs from spawning during the fight
      NPC.noTileCollide = true;
		}
    
    public override void AI()
    {
      int projType = ModContent.ProjectileType<Projectiles.CancerBossProjectile>();
      
      NPC.GravityMultiplier *= 0;
      
      if(AiState == StateSetup) {
        /*NPC minion = NPC.NewNPCDirect(
          NPC.GetSource_FromAI(),
          (int)NPC.Center.X,
          (int)NPC.Center.Y,
          ModContent.NPCType<HugeCancerBossTwoMembrane>(),
          NPC.whoAmI
        );
				if(minion.whoAmI != Main.maxNPCs) {
          //check for fail from spawn cap
          HugeCancerBossTwoMembrane castedMinion = (HugeCancerBossTwoMembrane)minion.ModNPC;
          castedMinion.AiParentIdx = NPC.whoAmI;
        }*/
        AiState = StateWait;
        AiTeleTimer = 1;
        AiTurnMult = (float)(Main.rand.NextDouble() * 2 - 1) * 0.025f;
      }
      NPC.TargetClosest(true);
      
      if(NPC.HasValidTarget)
      {
        Player thePlayer = Main.player[NPC.target];
        
        if(AiState == StateWait) {
          if((int)AiTeleTimer % (4 * 60) == 0) {
            double choice = Main.rand.NextDouble();
            
            if(choice > 0.5) {
              //ranges from 4 to 20 projectiles depending on current health
              AiTeleTimer = ((int)(NPC.lifeMax - NPC.life) / 1000 + 1) * 4 * 10;
              AiState = StateFire;
              
            } else if(choice > 0.25) {
              int moderate = ModContent.NPCType<ModerateCancerBossTwoMembrane>();
              int small = ModContent.NPCType<SmallCancerBossTwoMembrane>();
              int tiny = ModContent.NPCType<SoftshellMinicyte>();
              int cellChoice = Main.rand.Next(3);
              
              switch(cellChoice) {
                case 0:
                  NPC.NewNPCDirect(NPC.GetSource_FromAI(), NPC.Center, moderate, NPC.whoAmI, (float)Main.rand.NextDouble() * 6.28318530718f);
                  break;
                case 1:
                  for(int i = 0; i < 2; i++)
                    NPC.NewNPCDirect(NPC.GetSource_FromAI(), NPC.Center, small, NPC.whoAmI, (float)Main.rand.NextDouble() * 6.28318530718f);
                  break;
                case 2:
                  for(int i = 0; i < 2; i++)
                    NPC.NewNPCDirect(NPC.GetSource_FromAI(), NPC.Center, tiny, NPC.whoAmI, (float)Main.rand.NextDouble() * 6.28318530718f);
                  break;
              }
              SoundEngine.PlaySound(SoundID.Item16, NPC.Center);
              
            } else {
              double ang = Main.rand.NextDouble() * 6.28318530718;
              Vector2 offs = new Vector2((float)Math.Cos(ang), (float)Math.Sin(ang)) * 24 * 16;
              NPC.Center = thePlayer.Center + offs;
              SoundEngine.PlaySound(SoundID.Item4, NPC.Center);
            }
            AiTurnMult = (float)(Main.rand.NextDouble() * 2 - 1) * 0.05f;
          }
          AiTeleTimer++;
          
        } else if(AiState == StateFire) {
          if(AiTeleTimer > 0) {
            if(AiTeleTimer % 10 == 0) {
              double ang = Main.rand.NextDouble() * Math.PI * 2;
              Vector2 dir = new Vector2((float)Math.Cos(ang), (float)Math.Sin(ang));
              
              Vector2 pos = dir * 70 + NPC.Center;
              Vector2 vel = dir * ((float)Main.rand.NextDouble() * 6 + 4);
              
              Projectile.NewProjectile(NPC.GetSource_FromAI(), pos, vel, projType, 35, 0.125f, Main.myPlayer);
              SoundEngine.PlaySound(SoundID.Item17, NPC.Center);
            }
            AiTeleTimer--;
          } else {
            AiState = StateWait;
            AiTeleTimer = 1;
          }
        }
        
        float orbitAng = NPC.AngleTo(thePlayer.Center) + (float)(Math.PI / 2);
        //turn different dir once hit terrain
        //maybe don't turn at all?
        NPC.velocity += new Vector2((float)Math.Cos(orbitAng), (float)Math.Sin(orbitAng)) * AiTurnMult;
        
        float dist = NPC.Distance(thePlayer.Center);
        float desiredDistance = 16 * 16;
        //make this non-linear somehow? sqrt? also show model of this in desmos
        NPC.velocity += NPC.DirectionTo(thePlayer.Center) * (dist - desiredDistance) * 0.001f;
        
        NPC.velocity *= 0.99f;
      }
    }
    
    public override bool? CanFallThroughPlatforms() {
      return true;
    }
    
    public override void OnKill() {
      NPC.downedBoss2 = true;
    }
    
	}
}
