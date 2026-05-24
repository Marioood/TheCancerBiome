using Terraria;
using Terraria.ModLoader;

namespace TheCancerBiome.Content.Items
{ 
	public class Cytoplasm : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 25;
		}
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 20;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.buyPrice(silver: 1);
		}
	}
}
