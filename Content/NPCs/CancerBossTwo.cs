using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using TheCancerBiome.Content.Items;
using TheCancerBiome.Content.Projectiles;
using TheCancerBiome.Content.Dusts;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria.Localization;
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
    public ref float AiNextState => ref NPC.ai[3];
    public float AiDestX = 0;
    public float AiDestY = 0;
    
    public const float StateSetup = 0;
    public const float StateWait = 1;
    public const float StateFire = 2;
    
    public const float ChangeStateTime = (4 * 60);
    
		public override void SendExtraAI(BinaryWriter writer) {
			writer.Write(AiDestX);
			writer.Write(AiDestY);
		}

		public override void ReceiveExtraAI(BinaryReader reader) {
			AiDestY = reader.ReadSingle();
			AiDestX = reader.ReadSingle();
		}
    
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
			NPC.HitSound = SoundID.NPCHit9;
			NPC.DeathSound = SoundID.NPCDeath23;
			NPC.value = 5 * 100 * 100;
			NPC.knockBackResist = 0;
      NPC.aiStyle = -1;
			NPC.boss = true;
			NPC.npcSlots = 10;
      NPC.noTileCollide = true;
		}
    
    public override void AI()
    {
      int projType = ModContent.ProjectileType<Projectiles.CancerBossProjectile>();
      
      NPC.GravityMultiplier *= 0;
      
      if(AiState == StateSetup) {
        AiState = StateWait;
        AiNextState = (float)Main.rand.NextDouble();
        AiDestX = NPC.Center.X;
        AiDestY = NPC.Center.Y;
          
        AiTeleTimer = 1;
        AiTurnMult = (float)(Main.rand.NextDouble() * 2 - 1) * 0.025f;
      }
      NPC.TargetClosest(true);
    
      Player thePlayer = Main.player[NPC.target];
      
      if (thePlayer.dead) {
        NPC.velocity.Y += 0.1f;
        NPC.EncourageDespawn(10);
        return;
      }
      
      if(Main.netMode != NetmodeID.MultiplayerClient)
      {
        
        if(AiState == StateWait) {
          int modularTimer = (int)(AiTeleTimer % ChangeStateTime);
          
          if(AiNextState <= 0.25 && modularTimer > ChangeStateTime / 2) {
            Dust dust = Dust.NewDustDirect(new Vector2(AiDestX - NPC.width / 2, AiDestY - NPC.height / 2), NPC.width, NPC.height, ModContent.DustType<CancerStoneDust>());
            
            dust = Dust.NewDustDirect(NPC.TopLeft, NPC.width, NPC.height, ModContent.DustType<CancerStoneDust>());
          }
          
          if(modularTimer == 0) {
            double choice = AiNextState;//Main.rand.NextDouble();
            AiNextState = (float)Main.rand.NextDouble();
            
            if(AiNextState <= 0.25f) {
              double ang = Main.rand.NextDouble() * 6.28318530718;
              Vector2 offs = new Vector2((float)Math.Cos(ang), (float)Math.Sin(ang)) * 24 * 16;
              Vector2 pos = thePlayer.Center + offs;
              AiDestX = pos.X;
              AiDestY = pos.Y;
            }
            
            if(choice > 0.5f) {
              //ranges from 4 to 20 projectiles depending on current health
              AiTeleTimer = ((int)(NPC.lifeMax - NPC.life) / 1000 + 1) * 4 * 10;
              AiState = StateFire;
              
            } else if(choice > 0.25f) {
              int moderate = ModContent.NPCType<ModerateCancerBossTwoMembrane>();
              int small = ModContent.NPCType<SmallCancerBossTwoMembrane>();
              int tiny = ModContent.NPCType<SoftshellMinicyte>();
              int cellChoice = Main.rand.Next(3);
              
              if(cellChoice == 0) {
                for(int i = 0; i < 2; i++)
                  NPC.NewNPCDirect(NPC.GetSource_FromAI(), NPC.Center, tiny, NPC.whoAmI, (float)Main.rand.NextDouble() * 6.28318530718f);
              } else {
                NPC.NewNPCDirect(NPC.GetSource_FromAI(), NPC.Center, moderate, NPC.whoAmI, (float)Main.rand.NextDouble() * 6.28318530718f);
              }
              SoundEngine.PlaySound(SoundID.Item16, NPC.Center);
              
            } else {
              //teleport
              NPC.Center = new Vector2(AiDestX, AiDestY);
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
        NPC.velocity += NPC.DirectionTo(thePlayer.Center) * (dist - desiredDistance) * 0.001f;
        //limit speed so that boss doesn't unfairly slam into players at LUDICRIS SPEEDS!
        float len = NPC.velocity.Length();
        if(len > 6) {
          NPC.velocity /= len;
          NPC.velocity *= 6;
        }
        
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
