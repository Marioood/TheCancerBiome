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
	public class FatHead : ModNPC
	{
    public int AiAnimTimer = 0;
    
		public override void SetStaticDefaults() {
			Main.npcFrameCount[Type] = 4;
		}
    
		public override void SetDefaults()
		{
			NPC.width = 32;
			NPC.height = 32;
			NPC.damage = 18;
			NPC.defense = 6;
			NPC.lifeMax = 35;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 75f;
			NPC.knockBackResist = 0.95f;
      NPC.aiStyle = -1;
      
      Banner = Type;
      BannerItem = ModContent.ItemType<FatHeadBanner>();
		}
    
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
      npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LipoidalClump>(), 3, 1));
    }
    
    public override void AI()
    {
      NPC.GravityMultiplier *= 0;
      
      NPC.TargetClosest(true);
      
      if(NPC.HasValidTarget)
      {
        Player thePlayer = Main.player[NPC.target];
        
        Vector2 dirTo = NPC.DirectionTo(thePlayer.Center);
        
        NPC.velocity += dirTo * 0.05f;
      }
      NPC.velocity *= 0.999f;
      
      if(NPC.collideX) {
        NPC.velocity.X *= -1f;
      }
      if(NPC.collideY) {
        NPC.velocity.Y *= -1f;
      }
      //NPC.rotation += (NPC.velocity.X * NPC.velocity.Y) * 0.1f;
    }
    
    public override void FindFrame(int frameHeight) {
      NPC.frame.Y = (AiAnimTimer / 10 % 4) * frameHeight;
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
        var entSrc = NPC.GetSource_Death();
        
        for(int i = 0; i < 3; i++) {
          Gore.NewGore(entSrc, NPC.position, Vector2.Zero, gore1);
        }
        Gore.NewGore(entSrc, NPC.position, Vector2.Zero, gore2);
      }
    }
	}
}
