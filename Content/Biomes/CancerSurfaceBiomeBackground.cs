using Terraria.ModLoader;
using Terraria;

namespace TheCancerBiome.Backgrounds
{
	public class CancerSurfaceBiomeBackground : ModSurfaceBackgroundStyle
	{
    //fade color 84C7FF
    private int variantCount = 4;
    private int variant = 0;

    private int getVariant() {
      return (int)(Main.LocalPlayer.VisualPosition.X / 16 / Main.maxTilesX * variantCount);
    }
    
		public override void ModifyFarFades(float[] fades, float transitionSpeed) {
			for (int i = 0; i < fades.Length; i++) {
				if (i == Slot) {
          if(fades[i] == 0) {
            variant = getVariant();
          }
					fades[i] += transitionSpeed;
					if (fades[i] > 1f) {
						fades[i] = 1f;
					}
				}
				else {
					fades[i] -= transitionSpeed;
					if (fades[i] < 0f) {
						fades[i] = 0f;
					}
				}
			}
		}
		public override int ChooseFarTexture() {
			return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Assets/Textures/Backgrounds/CancerSurfaceFar");
		}

		public override int ChooseMiddleTexture() {
      return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Assets/Textures/Backgrounds/CancerSurfaceMid2");
		}
    
		public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b) {
      return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Assets/Textures/Backgrounds/CancerSurfaceClose2");
		}
	}
}