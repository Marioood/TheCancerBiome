using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace TheCancerBiome.Content.Tiles
{
	public class CancerCactus : ModCactus
	{
		private Asset<Texture2D> texture;
		private Asset<Texture2D> fruitTexture;

		public override void SetStaticDefaults() {
			GrowsOnTileId = [ModContent.TileType<Lumpsand>()];
			texture = ModContent.Request<Texture2D>("TheCancerBiome/Content/Tiles/CancerCactus");
			fruitTexture = ModContent.Request<Texture2D>("TheCancerBiome/Content/Tiles/CancerCactus_Fruit");
		}

		public override Asset<Texture2D> GetTexture() => texture;

		public override Asset<Texture2D> GetFruitTexture() => fruitTexture;
	}
}