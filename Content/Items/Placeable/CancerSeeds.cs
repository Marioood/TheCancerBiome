using Terraria;
using Terraria.ModLoader;

namespace TheCancerBiome.Content.Items.Placeable
{ 
	public class CancerSeeds : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 25;
		}
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.buyPrice(silver: 1);
		}
	}
}
