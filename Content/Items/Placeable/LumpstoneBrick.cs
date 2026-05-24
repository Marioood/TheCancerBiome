using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheCancerBiome.Content.Tiles;

namespace TheCancerBiome.Content.Items.Placeable
{ 
	public class LumpstoneBrick : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.LumpstoneBrick>());
		}
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<Placeable.Lumpstone>(2)
        .AddTile(TileID.Furnaces)
				.Register();
		}
	}
}
