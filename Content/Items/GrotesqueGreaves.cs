using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TheCancerBiome.Content.Items
{
	[AutoloadEquip(EquipType.Legs)]
	public class GrotesqueGreaves : ModItem
	{
		public static readonly int ArmorPenetration = 4;
    
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ArmorPenetration);
    
		public override void SetDefaults() {
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.defense = 6;
		}

		public override void UpdateEquip(Player player) {
      player.GetArmorPenetration(DamageClass.Generic) += ArmorPenetration;
			player.buffImmune[BuffID.Slow] = true;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<Placeable.TumorineBar>(20)
				.AddIngredient<Cytoplasm>(15)
        .AddTile(TileID.Anvils)
				.Register();
		}
	}
}