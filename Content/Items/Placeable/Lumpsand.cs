using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheCancerBiome.Content.Tiles;
using TheCancerBiome.Content.Projectiles;

namespace TheCancerBiome.Content.Items.Placeable
{ 
	public class Lumpsand : ModItem
	{
    public override void SetStaticDefaults() {
      
			ItemID.Sets.SandgunAmmoProjectileData[Type] = new(ModContent.ProjectileType<Projectiles.LumpsandBallGunProjectile>(), 10);
    }
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = Item.buyPrice(copper: 0);
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Lumpsand>());
			Item.ammo = AmmoID.Sand;
			Item.notAmmo = true;
		}
	}
}
