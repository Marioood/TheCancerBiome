using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheCancerBiome.Content.Tiles;

namespace TheCancerBiome.Content.Items.Placeable
{ 
	public class TumorineOre : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 100;
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = Item.buyPrice(silver: 10);
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Tumorine>());
		}
	}
}
