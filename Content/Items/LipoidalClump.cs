using Terraria;
using Terraria.ModLoader;

namespace TheCancerBiome.Content.Items
{ 
	public class LipoidalClump : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 24;
		}
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 22;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.buyPrice(copper: 3);
		}
	}
}
