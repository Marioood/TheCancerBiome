using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TheCancerBiome.Content.Items
{
	[AutoloadEquip(EquipType.Body)]
	public class GrotesqueCuirass : ModItem
	{
		public static readonly int ArmorPenetration = 5;
    
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ArmorPenetration);

		public override void SetDefaults() {
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.defense = 7;
		}

		public override void UpdateEquip(Player player) {
      player.GetArmorPenetration(DamageClass.Generic) += ArmorPenetration;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<Placeable.TumorineBar>(25)
				.AddIngredient<Cytoplasm>(20)
        .AddTile(TileID.Anvils)
				.Register();
		}
	}
}