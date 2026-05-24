using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheCancerBiome.Content.Dusts;
using TheCancerBiome.Content.Tiles;

namespace TheCancerBiome.Content.Items.Placeable
{ 
	public class OrangeIce : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.OrangeIce>());
		}
	}
}
