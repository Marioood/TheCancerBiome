using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheCancerBiome.Content.Projectiles;

namespace TheCancerBiome.Content.Items
{ 
	public class BouncyBall : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 32;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 12;
			Item.height = 12;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(silver: 160);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
      Item.shoot = ModContent.ProjectileType<Projectiles.BouncyBall>();
      Item.shootSpeed = 12f;
			Item.noMelee = true;
      Item.knockBack = 4;
			Item.consumable = true;
			Item.maxStack = Item.CommonMaxStack;
		}
    
		public override void AddRecipes() {
			CreateRecipe(50)
				.AddIngredient<Placeable.TumorineBar>(5)
				.AddIngredient<LipoidalClump>(20)
        .AddTile(TileID.Anvils)
				.Register();
		}
	}
}
