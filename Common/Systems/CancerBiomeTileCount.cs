using TheCancerBiome.Content.Tiles;
using System;
using Terraria.ModLoader;

namespace TheCancerBiome.Common.Systems
{
	public class CancerBiomeTileCount : ModSystem
	{
		public int cancerBlockCount;

		public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts) {
      int sum = 0;
			sum += tileCounts[ModContent.TileType<CancerStone>()];
			sum += tileCounts[ModContent.TileType<CancerGrass>()];
      
      cancerBlockCount = sum;
		}
	}
}