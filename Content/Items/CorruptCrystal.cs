using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheCancerBiome.Content.Tiles;

namespace TheCancerBiome.Content.Items
{ 
	public class CorruptCrystal : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = Item.buyPrice(silver: 5);
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.CorruptCrystal>());
		}
	}
}
