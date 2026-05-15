using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheCancerBiome.Content.Items
{ 
	public class GrotesquePickaxe : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 13;
			Item.DamageType = DamageClass.Melee;
			Item.width = 38;
			Item.height = 38;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = Item.buyPrice(silver: 40);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

			Item.pick = 65;
			Item.attackSpeedOnlyAffectsWeaponAnimation = true;
		}
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient<TumorineBar>(12)
				.AddIngredient<CancerBossItem>(6)
        .AddTile(TileID.Anvils)
				.Register();
		}
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) {
      target.AddBuff(BuffID.Slow, 10 * 60);
    }
	}
}
