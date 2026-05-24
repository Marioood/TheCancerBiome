using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheCancerBiome.Content.Tiles;

namespace TheCancerBiome.Content.Items.Placeable
{ 
	public class Lumpstone : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = Item.buyPrice(copper: 0);
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Lumpstone>());
		}
	}
}
