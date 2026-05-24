using TheCancerBiome.Content.Tiles;
using Terraria.ModLoader;
using Terraria.Enums;
using Terraria;

namespace TheCancerBiome.Content.Items.Placeable
{
  public class ImmunocyteBanner : ModItem
  {
    public override void SetDefaults()
    {
      Item.DefaultToPlaceableTile(
        ModContent.TileType<EnemyBanner>(),
        (int)EnemyBanner.StyleId.Immunocyte
      );
      Item.width = 12;
      Item.height = 28;
      Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(silver: 10));
    }
  }
}