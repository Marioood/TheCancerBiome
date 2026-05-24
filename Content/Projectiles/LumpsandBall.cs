using TheCancerBiome.Content.Items.Placeable;
using TheCancerBiome.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheCancerBiome.Content.Projectiles
{
	public abstract class LumpsandBallProjectile : ModProjectile
	{
		public override string Texture => "TheCancerBiome/Content/Projectiles/LumpsandBall";

		public override void SetStaticDefaults() {
			ProjectileID.Sets.FallingBlockDoesNotFallThroughPlatforms[Type] = true;
			ProjectileID.Sets.ForcePlateDetection[Type] = true;
		}
	}

	public class LumpsandBallFallingProjectile : LumpsandBallProjectile
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			ProjectileID.Sets.FallingBlockTileItem[Type] = new(ModContent.TileType<Tiles.Lumpsand>(), ModContent.ItemType<Items.Placeable.Lumpsand>());
		}

		public override void SetDefaults() {
			// The falling projectile when compared to the sandgun projectile is hostile.
			Projectile.CloneDefaults(ProjectileID.EbonsandBallFalling);
		}
	}

	public class LumpsandBallGunProjectile : LumpsandBallProjectile
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			ProjectileID.Sets.FallingBlockTileItem[Type] = new(ModContent.TileType<Tiles.Lumpsand>());
		}

		public override void SetDefaults() {
			// The sandgun projectile when compared to the falling projectile has a ranged damage type, isn't hostile, and has extraupdates = 1.
			// Note that EbonsandBallGun has infinite penetration, unlike SandBallGun
			Projectile.CloneDefaults(ProjectileID.EbonsandBallGun);
			AIType = ProjectileID.EbonsandBallGun; // This is needed for some logic in the ProjAIStyleID.FallingTile code.
		}
	}
}