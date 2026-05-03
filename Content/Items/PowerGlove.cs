using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheCancerBiome.Content.Projectiles;

namespace TheCancerBiome.Content.Items
{ 
	public class PowerGlove : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 72;
			Item.DamageType = DamageClass.Magic;
			Item.width = 26;
			Item.height = 28;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.value = Item.buyPrice(silver: 160);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item43;
			Item.autoReuse = true;
      Item.shoot = ModContent.ProjectileType<PowerGloveHand>();
      Item.shootSpeed = 4f;
      Item.mana = 12;
			Item.noMelee = true;
      Item.knockBack = 2;
		}
	}
}
