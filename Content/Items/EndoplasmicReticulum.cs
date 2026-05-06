using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheCancerBiome.Content.Items
{ 
	public class EndoplasmicReticulum : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 18;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 56;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.value = Item.buyPrice(silver: 12);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
      Item.scale = 1.25f;
		}
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<TumorineBar>(10)
        .AddTile(TileID.Anvils)
				.Register();
		}
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) {
      target.AddBuff(BuffID.Slow, 10 * 60);
    }
	}
}
