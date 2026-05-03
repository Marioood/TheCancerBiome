using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheCancerBiome.Content.Projectiles;

namespace TheCancerBiome.Content.Items
{ 
	public class HandCannon : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 26;
			Item.height = 28;
			Item.useTime = 45;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.value = Item.buyPrice(silver: 160);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item43;
			Item.autoReuse = false;
      Item.shoot = ProjectileID.PurificationPowder;
      Item.shootSpeed = 12;
			Item.noMelee = true;
      Item.knockBack = 6;
      Item.useAmmo = AmmoID.Bullet;
		}
	}
}
