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
	public class TinyCancerBossTwoMembrane : HugeCancerBossTwoMembrane
	{
		public override void SetDefaults()
		{
      PanSetup();
      
			NPC.width = 32;
			NPC.height = 32;
			NPC.damage = 12;
      NPC.lifeMax = 25;
		}
    public override void OnKill() { }
	}
}
