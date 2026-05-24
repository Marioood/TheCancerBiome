using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using TheCancerBiome.Content.Items.Placeable;
using TheCancerBiome.Content.Items;
using TheCancerBiome.Content.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;

namespace TheCancerBiome.Content.NPCs
{ 
	public class Immunocyte : ModNPC
	{
    public ref float AiStrafeAng => ref NPC.ai[0];
    public ref float AiAngTimer => ref NPC.ai[1];
    public ref float AiChargeTimer => ref NPC.ai[2];
    
    public int AiAnimTimer = 0;
    public bool AiNeedsSetup = true;
    
    private SoundStyle RoarSound = new SoundStyle("TheCancerBiome/Assets/Sounds/ShankerCharge") {
      Volume = 0.5f,
      PitchVariance = 1f,
      MaxInstances = 3
    };
    
		public override void SetStaticDefaults() {
			Main.npcFrameCount[Type] = 4;
		}
    
		public override void SetDefaults()
		{
			NPC.width = 42;
			NPC.height = 42;
			NPC.damage = 40;
			NPC.defense = 6;
			NPC.lifeMax = 67;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 250f;
			NPC.knockBackResist = 0.8f;
      NPC.aiStyle = -1;
      
      Banner = Type;
      BannerItem = ModContent.ItemType<ImmunocyteBanner>();
		}
    
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
      npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LipoidalClump>(), 3, 1));
    }
    
    public override void AI()
    {
      if(AiNeedsSetup) {
        AiChargeTimer = 10 * 60;
        AiAnimTimer = 10 * 60;
        AiNeedsSetup = false;
      }
      NPC.GravityMultiplier *= 0;
      AiAngTimer += 0.01f;
      AiStrafeAng = (float)(Math.Sin(AiAngTimer) * Math.PI * 2);
      AiStrafeAng += (NPC.Center.X + NPC.Center.Y) * 0.005f;
      
      NPC.TargetClosest(true);
      
      if(NPC.HasValidTarget)
      {
        Player thePlayer = Main.player[NPC.target];
        Vector2 dirTo = NPC.DirectionTo(thePlayer.Center);
        float angTo = NPC.AngleTo(thePlayer.Center);
        NPC.rotation = angTo - (float)Math.PI / 2;
        
        Vector2 strafeDir = Vector2.Zero;
        float strafeMag = 0.1f;
        float dist = NPC.Distance(thePlayer.Center);
        if(dist < 20 * 16) {
          strafeDir.X += (float)Math.Cos(AiStrafeAng + angTo) * strafeMag;
          strafeDir.Y += (float)Math.Sin(AiStrafeAng + angTo) * strafeMag;
        } else {
          strafeDir += dirTo * strafeMag;
        }
        NPC.velocity += strafeDir;
        
        if(AiChargeTimer == 0) {
          if(Main.netMode != NetmodeID.MultiplayerClient) {
            AiChargeTimer = ((float)Main.rand.NextDouble() * 5 + 5) * 60;
          }
          NPC.velocity = dirTo * 16;
          AiAnimTimer = 0;
          SoundEngine.PlaySound(RoarSound, NPC.Center);
        }
      }
      NPC.velocity *= 0.99f;
      
      if(NPC.collideX) {
        NPC.velocity.X *= -1f;
      }
      if(NPC.collideY) {
        NPC.velocity.Y *= -1f;
      }
      
      AiChargeTimer--;
    }
    
    public override void FindFrame(int frameHeight) {
      NPC.frame.Y = (AiAnimTimer / 8) % 3 * frameHeight;
      if(AiAnimTimer <= 1 * 60) {
        NPC.frame.Y = 3 * frameHeight;
      }
      AiAnimTimer++;
    }
    
    public override bool? CanFallThroughPlatforms() {
      return true;
    }
    
		public override void HitEffect(NPC.HitInfo hit) {
			if(Main.netMode == NetmodeID.Server) {
        return;
      }
      for(int i = 0; i < 8; i++) {
        Dust.NewDustDirect(NPC.TopLeft, NPC.width, NPC.height, ModContent.DustType<OrangeCellDust>());
      }
      
      if(NPC.life <= 0) {
        for(int i = 0; i < 16; i++) {
          Dust.NewDustDirect(NPC.TopLeft, NPC.width, NPC.height, ModContent.DustType<OrangeCellDust>());
        }
        int gore1 = Mod.Find<ModGore>("OrangeCellGore1").Type;
        int gore2 = Mod.Find<ModGore>("NucleusGore1").Type;
        int gore3 = Mod.Find<ModGore>("ImmunocyteGore").Type;
        var entSrc = NPC.GetSource_Death();
        
        for(int i = 0; i < 4; i++) {
          Gore.NewGore(entSrc, NPC.position, Vector2.Zero, gore1);
        }
        Gore.NewGore(entSrc, NPC.position, Vector2.Zero, gore2);
        Gore.NewGore(entSrc, NPC.position, Vector2.Zero, gore3);
      }
    }
	}
}
