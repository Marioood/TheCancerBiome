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
	public class CancerBoss : ModNPC
	{
    public ref float AiState => ref NPC.ai[0];
    public ref float AiAttackTimer => ref NPC.ai[1];
    public ref float AiPreviousStage => ref NPC.ai[2];
    public ref float AiMinionsSpawnedPerAttack => ref NPC.ai[3];
    
    private float BossStatePreSetup = 0;
    private float BossStateFireProjectiles = 1;
    private float BossStatePillarWait = 2;
    private float BossStateDoPillar = 3;
    private float BossStateDoDash = 4;
    
		public override void SetStaticDefaults() {
			Main.npcFrameCount[Type] = 2;
			NPCID.Sets.BossBestiaryPriority.Add(Type);
			NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		}
    
		public override void SetDefaults()
		{
			NPC.width = 168;
			NPC.height = 150;
			NPC.damage = 50;
			NPC.defense = 12;
			NPC.lifeMax = 2000;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath23;
			NPC.value = 5 * 100 * 100;
			NPC.knockBackResist = 0;
      NPC.aiStyle = -1;
			NPC.boss = true;
			NPC.npcSlots = 10; // Take up open spawn slots, preventing random NPCs from spawning during the fight
		}
    
    public override void AI()
    {
      int projType = ModContent.ProjectileType<Projectiles.CancerBossProjectile>();
      if(NPC.collideY) NPC.velocity.X *= 0.75f;
      
      NPC.TargetClosest(true);
      
      if(NPC.HasValidTarget)
      {
        Player thePlayer = Main.player[NPC.target];
        
        if(AiState == BossStatePreSetup) {
          AiState = BossStatePillarWait;
          
        } else if(AiState == BossStateFireProjectiles) {
          
          if((int)(AiAttackTimer % 20) == 0) {
            double ang = Main.rand.NextDouble() * Math.PI * 2;
            Vector2 dir = new Vector2((float)Math.Cos(ang), (float)Math.Sin(ang));
            if(dir.Y > 0) dir.Y *= -1; //always on top of the thingaling
            
            Vector2 pos = dir * 70 + NPC.Center;
            Vector2 vel = dir * ((float)Main.rand.NextDouble() * 4 + 8);
            
            Projectile.NewProjectile(NPC.GetSource_FromAI(), pos, vel, projType, 35, 0.125f, Main.myPlayer);
          }
          
          if(AiAttackTimer > 3 * 60) {
            AiState = BossStatePillarWait;
            AiAttackTimer = 0;
          }
          AiAttackTimer++;
          
        } else if(AiState == BossStatePillarWait) {
          if(AiAttackTimer > 2 * 60 && NPC.collideY) {
            if(IsSecondStage()) {
              AiState = Main.rand.Next(2) == 0 ? BossStateDoPillar : BossStateDoDash;
            } else {
              AiState = Main.rand.Next(2) == 0 ? BossStateDoPillar : BossStateFireProjectiles;
            }
            AiAttackTimer = 0;
          }
          AiAttackTimer++;
          
        } else if(AiState == BossStateDoPillar) {
          if(AiAttackTimer > 2 * 60 && NPC.Center.Y < thePlayer.Center.Y - 16 * 16) {
            AiState = BossStatePillarWait;
            AiAttackTimer = 0;
            AiMinionsSpawnedPerAttack = 0;
          } else {
            NPC.GravityMultiplier *= 0;
            NPC.velocity.Y = -0.25f * 16;
            NPC.velocity.X += (NPC.Center.X < thePlayer.Center.X) ? 0.1f : -0.1f;
            
            if(Main.rand.Next(60) == 0 && AiMinionsSpawnedPerAttack <= 3) {
              int fatty = IsSecondStage() ? ModContent.NPCType<SoftshellMinicyte>() : ModContent.NPCType<FatHead>();
              int id = Main.rand.Next(4) == 0 ? fatty : ModContent.NPCType<CancerBossMinion>();
              NPC minion = NPC.NewNPCDirect(NPC.GetSource_FromAI(), NPC.Center, id);
              AiMinionsSpawnedPerAttack++;
            }
          }
          AiAttackTimer++;
          
        } else if(AiState == BossStateDoDash) {
          NPC.GravityMultiplier *= NPC.velocity.Y > 0 ? 2 : 0.5f;
          if(AiAttackTimer == 2 * 60) { 
            NPC.velocity.Y = -10;
          }
          if(AiAttackTimer > 4 * 60 && NPC.collideX) {
            int thingCount = 8;
            for(int i = 0; i < thingCount; i++) {
              double ang = Main.rand.NextDouble() * Math.PI * 2;
              Vector2 dir = new Vector2((float)Math.Cos(ang), (float)Math.Sin(ang));
              if(dir.Y > 0) dir.Y *= -1; //always on top of the thingaling
              
              Vector2 pos = dir * 70 + NPC.Center;
              
              if(Main.rand.Next(4) == 0) {
                int id = ModContent.NPCType<SoftshellMinicyte>();
                NPC minion = NPC.NewNPCDirect(NPC.GetSource_FromAI(), pos, id);
              } else {
                Vector2 vel = dir * ((float)Main.rand.NextDouble() * 4 + 8);
                
                Projectile.NewProjectile(NPC.GetSource_FromAI(), pos, vel, projType, 35, 0.125f, Main.myPlayer);
              }
            }
            
            AiState = BossStateFireProjectiles;
            AiAttackTimer = 0;
          }
          AiAttackTimer++;
        }
      } else {
        //TODO: escape animation
      }
      
      //transition from first 2 second stage
      if(IsSecondStage() && AiPreviousStage == 0) {
        NPC.HitSound = SoundID.NPCHit1;
      }
      
      AiPreviousStage = IsSecondStage() ? 1 : 0;
    }
    
    public override bool? CanFallThroughPlatforms() {
      NPC.TargetClosest(true);
      
      if(NPC.HasValidTarget)
      {
        Player thePlayer = Main.player[NPC.target];
        
        if(thePlayer.Center.Y > NPC.Center.Y) {
          return true;
        }
      }
      
      return false;
    }
    
    public override void FindFrame(int frameHeight) {
      NPC.frame.Y = (IsSecondStage() ? 1 : 0) * frameHeight;
    }
    
    private bool IsSecondStage() {
      return NPC.life < 1000;
    }
    
    public override void OnKill() {
      NPC.downedBoss2 = true;
    }
    
	}
}
