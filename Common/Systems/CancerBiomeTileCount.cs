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
			sum += tileCounts[ModContent.TileType<Lumpstone>()];
			sum += tileCounts[ModContent.TileType<CancerGrass>()];
			sum += tileCounts[ModContent.TileType<Lumpsand>()];
      
      cancerBlockCount = sum;
		}
	}
}