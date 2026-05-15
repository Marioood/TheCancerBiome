using TheCancerBiome.Backgrounds;
using TheCancerBiome.Common.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheCancerBiome.Content.Biomes
{
	// Shows setting up two basic biomes. For a more complicated example, please request.
	public class CancerSurfaceBiome : ModBiome
	{
		// Select all the scenery
		public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<CancerSurfaceBiomeBackground>();
		public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Crimson;

		
		//public override ModWaterStyle WaterStyle => ModContent.GetInstance<ExampleWaterStyle>(); // Sets a water style for when inside this biome
    
    
    
    // Select Music
		public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/SurfaceTemp");
		/* If you need the music choice to be conditional, such as supporting the Otherworld soundtrack toggle, you can use this approach:
		public override int Music {
			get {
				if (!Main.swapMusic == Main.drunkWorld && !Main.remixWorld) {
					return MusicID.OtherworldlyEerie;
				}
				return MusicLoader.GetMusicSlot(Mod, "Assets/Music/MysteriousMystery");
			}
		}
		*/

		/*public override int BiomeTorchItemType => ModContent.ItemType<ExampleTorch>();
		public override int BiomeCampfireItemType => ModContent.ItemType<ExampleCampfire>();

		// Populate the Bestiary Filter
		public override string BestiaryIcon => base.BestiaryIcon;
		public override string BackgroundPath => base.BackgroundPath;
		public override Color? BackgroundColor => base.BackgroundColor;
		public override string MapBackground => BackgroundPath; // Re-uses Bestiary Background for Map Background*/

		public override bool IsBiomeActive(Player player) {
			bool hasBlocks = ModContent.GetInstance<CancerBiomeTileCount>().cancerBlockCount >= 40;

			// Finally, we will limit the height at which this biome can be active to above ground (ie sky and surface). Most (if not all) surface biomes will use this condition.
			bool isSurface = player.ZoneSkyHeight || player.ZoneOverworldHeight;
			return hasBlocks && isSurface;
		}
	}
}