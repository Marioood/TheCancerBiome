using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TheCancerBiome.Content.Items
{
	[AutoloadEquip(EquipType.Head)]
	public class GrotesqueHelmet : ModItem
	{
		public static LocalizedText SetBonusText { get; private set; }
    
    public static readonly float MoveSpeed = 0.5f;
		public static readonly int ArmorPenetration = 3;
    
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ArmorPenetration);
    
		public override void SetStaticDefaults() {
			SetBonusText = this.GetLocalization("SetBonus");
		}
    
		public override void SetDefaults() {
			Item.width = 24;
			Item.height = 18;
			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
			Item.defense = 6;
		}

		public override void UpdateEquip(Player player) {
      player.GetArmorPenetration(DamageClass.Generic) += ArmorPenetration;
		}
    
		public override bool IsArmorSet(Item head, Item body, Item legs) {
			return body.type == ModContent.ItemType<GrotesqueCuirass>() && legs.type == ModContent.ItemType<GrotesqueGreaves>();
		}

		public override void UpdateArmorSet(Player player) {
			player.setBonus = SetBonusText.Value;
      player.moveSpeed += MoveSpeed;
      player.jumpSpeedBoost += 0.75f;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<Placeable.TumorineBar>(15)
				.AddIngredient<Cytoplasm>(10)
        .AddTile(TileID.Anvils)
				.Register();
		}
	}
}