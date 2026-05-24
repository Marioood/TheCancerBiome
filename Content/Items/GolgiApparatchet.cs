using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheCancerBiome.Content.Items
{ 
	public class GolgiApparatchet : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 26;
			Item.DamageType = DamageClass.Melee;
			Item.width = 44;
			Item.height = 44;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.value = Item.buyPrice(silver: 27);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
      Item.axe = 85 / 5;
		}
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<Placeable.TumorineBar>(10)
        .AddTile(TileID.Anvils)
				.Register();
		}
	}
}
