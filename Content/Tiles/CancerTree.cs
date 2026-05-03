using TheCancerBiome.Content.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace TheCancerBiome.Content.Tiles
{
	public class CancerTree : ModTree
	{
		private Asset<Texture2D> texture;
		private Asset<Texture2D> branchesTexture;
		private Asset<Texture2D> topsTexture;

		// This is a blind copy-paste from Vanilla's PurityPalmTree settings.
		// TODO: This needs some explanations
		public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings {
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 11f / 72f,
			SpecialGroupMaximumHueValue = 0.25f,
			SpecialGroupMinimumSaturationValue = 0.88f,
			SpecialGroupMaximumSaturationValue = 1f
		};

		public override void SetStaticDefaults() {
			// Makes Example Tree grow on ExampleBlock
			GrowsOnTileId = [ModContent.TileType<CancerGrass>()];
			texture = ModContent.Request<Texture2D>("TheCancerBiome/Content/Tiles/CancerTree");
			branchesTexture = ModContent.Request<Texture2D>("TheCancerBiome/Content/Tiles/CancerTree_Branches");
			topsTexture = ModContent.Request<Texture2D>("TheCancerBiome/Content/Tiles/CancerTree_Tops");
		}

		// This is the primary texture for the trunk. Branches and foliage use different settings.
		public override Asset<Texture2D> GetTexture() {
			return texture;
		}

		/*public override int SaplingGrowthType(ref int style) {
			style = 0;
			return ModContent.TileType<Plants.ExampleSapling>();
		}*/
		// Branch Textures
		public override Asset<Texture2D> GetBranchTextures() => branchesTexture;

		// Top Textures
		public override Asset<Texture2D> GetTopTextures() => topsTexture;

		public override int DropWood() {
			return ModContent.ItemType<Items.Cellulose>();
		}

		/*public override bool Shake(int x, int y, ref bool createLeaves) {
			Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16, ModContent.ItemType<Items.Placeable.ExampleBlock>());
			return false;
		}

		public override int TreeLeaf() {
			return ModContent.GoreType<ExampleTreeLeaf>();
		}*/
	}
}