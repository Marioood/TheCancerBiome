using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheCancerBiome.Content.Items;

namespace TheCancerBiome.Content
{ 
	public class Recipes : ModSystem
	{
    public override void AddRecipes() {
      Recipe.Create(ItemID.DeerThing, 1)
        .AddIngredient<TumorineOre>(5)
        .AddIngredient(ItemID.FlinxFur, 3)
        .AddIngredient(ItemID.Lens, 1)
        .AddTile(TileID.DemonAltar)
        .Register();
        
      Recipe.Create(ItemID.NightsEdge, 1)
        .AddIngredient<EndoplasmicReticulum>(1)
        .AddIngredient(ItemID.BladeofGrass, 1)
        .AddIngredient(ItemID.FieryGreatsword, 1)
        .AddIngredient(ItemID.Muramasa, 1)
        .AddTile(TileID.Anvils)
        .Register();
        
      Recipe.Create(ItemID.Magiluminescence, 1)
        .AddIngredient<TumorineBar>(12)
        .AddIngredient(ItemID.Topaz, 5)
        .AddTile(TileID.Anvils)
        .Register();
      
      Recipe.Create(ItemID.VoidVault, 1)
        .AddIngredient<CancerBossItem>(15)
        .AddIngredient(ItemID.JungleSpores, 8)
        .AddIngredient(ItemID.Bone, 15)
        .AddTile(TileID.DemonAltar)
        .Register();
      
      Recipe.Create(ItemID.ClosedVoidBag, 1)
        .AddIngredient<CancerBossItem>(30)
        .AddIngredient(ItemID.JungleSpores, 15)
        .AddIngredient(ItemID.Bone, 30)
        .AddTile(TileID.DemonAltar)
        .Register();
      
      Recipe.Create(ItemID.ObsidianHelm, 1)
        .AddIngredient<CancerBossItem>(5)
        .AddIngredient(ItemID.Silk, 10)
        .AddIngredient(ItemID.Obsidian, 20)
        .AddTile(TileID.Hellforge)
        .Register();
      
      Recipe.Create(ItemID.ObsidianShirt, 1)
        .AddIngredient<CancerBossItem>(10)
        .AddIngredient(ItemID.Silk, 10)
        .AddIngredient(ItemID.Obsidian, 20)
        .AddTile(TileID.Hellforge)
        .Register();
      
      Recipe.Create(ItemID.ObsidianPants, 1)
        .AddIngredient<CancerBossItem>(5)
        .AddIngredient(ItemID.Silk, 10)
        .AddIngredient(ItemID.Obsidian, 20)
        .AddTile(TileID.Hellforge)
        .Register();
    }
	}
}
