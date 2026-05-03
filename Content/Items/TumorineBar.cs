using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace TheCancerBiome.Content.Items
{ 
	public class TumorineBar : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 25;
		}
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = Item.CommonMaxStack;
			Item.value = Item.buyPrice(silver: 30);
		}
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<TumorineOre>(2)
				.AddIngredient<CorruptCrystal>(1)
        .AddTile(TileID.Furnaces)
				.Register();
		}
	}
}
