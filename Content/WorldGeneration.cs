using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Generation;
using System.Collections.Generic;
using Terraria.WorldBuilding;
using Terraria.IO;
using Terraria.Localization;
using System;

namespace TheCancerBiome.Content
{
	public class WorldGenSystem : ModSystem
	{
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
			int corruptionIdx = tasks.FindIndex(genpass => genpass.Name.Equals("Corruption"));
      
			if (corruptionIdx != -1) {
				//tasks.RemoveAt(corruptionIdx);
        //same name so that other mods don't break
        tasks[corruptionIdx] = new CancerGenPass("Corruption", 100f);
			}
		}
	}

	public class CancerGenPass : GenPass
	{
    private ushort cancerStone = (ushort)ModContent.TileType<Tiles.Lumpstone>();
    private ushort tumorine = (ushort)ModContent.TileType<Tiles.Tumorine>();
    private ushort cancerGrass = (ushort)ModContent.TileType<Tiles.CancerGrass>();
    private ushort cancerSand = (ushort)ModContent.TileType<Tiles.Lumpsand>();
    private ushort cancerCrystal = (ushort)ModContent.TileType<Tiles.Struvite>();
    
    private ushort orbNucleus = (ushort)ModContent.TileType<Tiles.SwollenNucleus>();
    private ushort cancerAltar = (ushort)ModContent.TileType<Tiles.CancerAltar>();
    private ushort cancerTallGrass = (ushort)ModContent.TileType<Tiles.CancerTallGrass>();
    
    ushort tumorineWall = (ushort)ModContent.WallType<Walls.TumorineWall>();
    private ushort cancerStoneWall = (ushort)ModContent.WallType<Walls.CancerStoneWall>();
    
    private byte[,] noiseTable = null;
    private int voronoiSeed = WorldGen.genRand.Next(10000);
    
		public CancerGenPass(string name, float loadWeight) : base(name, loadWeight) { }

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
			progress.Message = "Spreading evil";
      
      int biomeWidth = Main.maxTilesX / 9;
      int biomeHeight = 400;
      int biomeCenterX = Main.maxTilesX / 2;
      //min cloud levels
      //small - 200
      //med - 160
      //large - 260
      
      int si = Main.maxTilesY / 6;
      
      for(; si < Main.maxTilesY; si++) {
        Tile tile = Main.tile[biomeCenterX, si];
        if(tile.HasTile && (tile.TileType == TileID.Dirt || tile.TileType == TileID.Stone || tile.TileType == TileID.Sand)) {
          break;
        }
      }
      
      int biomeCenterY = si;
      
      /*double biomeCenterYAvg = biomeCenterY;
      
      for(int i = 0; i < 256; i++) {
        int x = WorldGen.genRand.Next(biomeWidth / -2, biomeWidth / 2)   + biomeCenterX;
        int y = WorldGen.genRand.Next(biomeHeight / -2, biomeHeight / 2) + biomeCenterY;
        if(Main.tile[x, (int)(biomeCenterY)].HasTile) {
          if(!Main.tile[x, y].HasTile) {
            biomeCenterYAvg = (biomeCenterYAvg + y) / 2;
          }
          biomeCenterYAvg += 2;
        } else {
          if(Main.tile[x, y].HasTile) {
            biomeCenterYAvg = (biomeCenterYAvg + y) / 2;
          }
          biomeCenterYAvg += 2;
        }
      }
      biomeCenterY = (int)biomeCenterYAvg;*/
      
      List<int> cellXs = new List<int>();
      List<int> cellYs = new List<int>();
      List<int> cellRadii = new List<int>();
      
      List<int> crystalXs = new List<int>();
      List<int> crystalYs = new List<int>();
      List<int> crystalRadii = new List<int>();
      
      List<int> altarXs = new List<int>();
      List<int> altarYs = new List<int>();
      
      List<int> nucleusOrbXs = new List<int>();
      List<int> nucleusOrbYs = new List<int>();
      //underground tunnels
      //tunnels generate in V shape. abs(abs(x-3)/2-0.5) OR abs(x - 2) / 2
      for(int t = 0; t < 5; t++) {
        int rx = (biomeWidth * t / 5) - biomeWidth / 2 + biomeCenterX;//biomeCenterX + WorldGen.genRand.Next(biomeWidth) - (biomeWidth / 2);
        double offsY = Math.Abs(Math.Abs(t - 2) / 2.0);
        int ry = biomeCenterY - (int)(offsY * 200) + 180;//biomeCenterY + WorldGen.genRand.Next(biomeHeight) - 64;
        int tunnelRadius = WorldGen.genRand.Next(32) + 80;
        PlaceTunnels(rx, ry, tunnelRadius);
        int cellCount = tunnelRadius / 10;
        //add to a queue so that tunnels don't overwrite cells
        for(int c = 0; c < cellCount; c++) {
          cellXs.Add(rx + WorldGen.genRand.Next(tunnelRadius) - (tunnelRadius / 2));
          cellYs.Add(ry + WorldGen.genRand.Next(tunnelRadius) - (tunnelRadius / 2));
          int cellRadius = WorldGen.genRand.Next(8) + 4;
          if(WorldGen.genRand.Next(8) == 0) cellRadius += 4;
          cellRadii.Add(cellRadius);
        
          crystalXs.Add(rx + WorldGen.genRand.Next(tunnelRadius) - (tunnelRadius / 2));
          crystalYs.Add(ry + WorldGen.genRand.Next(tunnelRadius) - (tunnelRadius / 2));
          int r = WorldGen.genRand.Next(4) + 2;
          if(WorldGen.genRand.Next(12) == 0) r += WorldGen.genRand.Next(8) + 4;
          crystalRadii.Add(r);
        }
        int altarCount = tunnelRadius / 5;
        for(int c = 0; c < cellCount; c++) {
          altarXs.Add(rx + WorldGen.genRand.Next(tunnelRadius) - (tunnelRadius / 2));
          altarYs.Add(ry + WorldGen.genRand.Next(tunnelRadius) - (tunnelRadius / 2));
        }
        if(WorldGen.genRand.Next(3) == 0) {
          cellXs.Add(rx);
          cellYs.Add(ry);
          cellRadii.Add(tunnelRadius / 4);
        }
      }
      //overground tunnels
      /*for(int t = 0; t < 4; t++) {
        int rx = biomeCenterX + WorldGen.genRand.Next(biomeWidth) - (biomeWidth / 2);
        int ry = biomeCenterY - WorldGen.genRand.Next(32) + 16;
        int tunnelRadius = WorldGen.genRand.Next(32) + 64;
        PlaceTunnels(rx, ry, tunnelRadius);
        
        altarXs.Add(rx + WorldGen.genRand.Next(tunnelRadius) - (tunnelRadius / 2));
        altarYs.Add(ry + WorldGen.genRand.Next(tunnelRadius) - (tunnelRadius / 2));
        cellXs.Add(rx + WorldGen.genRand.Next(tunnelRadius) - (tunnelRadius / 2));
        cellYs.Add(ry + WorldGen.genRand.Next(tunnelRadius) - (tunnelRadius / 2));
        for(int cs = 0; cs < 4; cs++) {
          crystalXs.Add(rx + WorldGen.genRand.Next(tunnelRadius) - (tunnelRadius / 2));
          crystalYs.Add(ry + WorldGen.genRand.Next(tunnelRadius) - (tunnelRadius / 2));
        }
      }*/
      //underground cells
      for(int c = 0; c < cellXs.Count; c++) {
        PlaceCell(cellXs[c], cellYs[c], cellRadii[c], WorldGen.genRand.Next(4) != 0, nucleusOrbXs, nucleusOrbYs);
      }
      //giant central cell
      PlaceCell(biomeCenterX, biomeCenterY + 100, 72 + WorldGen.genRand.Next(8), true, nucleusOrbXs, nucleusOrbYs);
      //crystals
      for(int c = 0; c < crystalRadii.Count; c++) {
        PlaceCrystal(crystalXs[c], crystalYs[c], crystalRadii[c]);
      }
      //altars
      for(int c = 0; c < altarXs.Count; c++) {
        int ax = altarXs[c];
        int ay = altarYs[c];
        
        for(int i = 0; i < 64; i++) {
          if(Main.tile[ax, ay + i].HasTile) {
            ay += i;
            break;
          }
        }
        
        PlaceAltar(ax, ay);
      }
      //nucleus orbs
      for(int c = 0; c < nucleusOrbXs.Count; c++) {
        int nx = nucleusOrbXs[c];
        int ny = nucleusOrbYs[c];
        
        //placement check
        bool skipFlag = false;
        
        for(int y = 0; y < 2; y++) {
          for(int x = 0; x < 2; x++) {
            if(Main.tile[nx + x, ny + y].TileType == orbNucleus) skipFlag = true;
          }
        }
        if(skipFlag) continue;
        
        //cells can generate on the surface. having nuclei with very powerful weapons available that easily sounds bad to me
        bool oreNucleus = ny < biomeCenterY;
        if(oreNucleus) {
          for(int y = 0; y < 2; y++) {
            for(int x = 0; x < 2; x++) {
              Main.tile[nx + x, ny + y].ResetToType(tumorine);
            }
          }
        } else {
          for(int y = 0; y < 2; y++) {
            for(int x = 0; x < 2; x++) {
              Main.tile[nx + x, ny + y].ClearTile();
            }
          }
          
          WorldGen.PlaceTile(nx, ny, orbNucleus);
        }
      }
      
      //tile replacements
      for(int yi = -biomeHeight / 2; yi <= biomeHeight / 2; yi++) {
        for(int xi = -biomeWidth / 2; xi <= biomeWidth / 2; xi++) {
          int x = xi + biomeCenterX;
          int y = yi + biomeCenterY;
          Tile cur = Framing.GetTileSafely(x,y);
          
          double xN = x / (double)(biomeCenterX / 2);
          double yN = y / (double)(biomeCenterY / 2);
          double dist = xN * xN + yN * yN;
          //double noisyDist = (1 - dist * dist * dist * dist) * (WorldGen.genRand.NextDouble() * 0.5 + 0.5);
          
          if(!cur.HasTile) continue;
          //if(dist > 1) continue;
          
          if(cur.TileType == TileID.Grass) {
            cur.ResetToType(cancerGrass);
            
          } else if(cur.TileType == TileID.Stone) {
            cur.ResetToType(cancerStone);
            
          } else if(cur.TileType == TileID.Sand) {
            cur.ResetToType(cancerSand);
            
          } else if(cur.TileType == TileID.Dirt) {
            Tile top = Framing.GetTileSafely(x,y-1);
            Tile bot = Framing.GetTileSafely(x,y+1);
            Tile left = Framing.GetTileSafely(x-1,y);
            Tile right = Framing.GetTileSafely(x+1,y);
            
            bool airy = !top.HasTile || !bot.HasTile || !left.HasTile || !right.HasTile;
            
            if(airy) {
              cur.ResetToType(cancerGrass);
              if(WorldGen.genRand.Next(2) == 0 && !Framing.GetTileSafely(x, y-1).HasTile) {
                Framing.GetTileSafely(x, y-1).ResetToType(cancerTallGrass);
              }
            }
          } else if(
            cur.TileType == TileID.Copper
            || cur.TileType == TileID.Tin
            || cur.TileType == TileID.Iron
            || cur.TileType == TileID.Lead
            || cur.TileType == TileID.Gold
            || cur.TileType == TileID.Platinum 
          ) {
            cur.ResetToType(cancerCrystal);
          }
          
          if(cur.WallType == WallID.Stone) {
            cur.WallType = cancerStoneWall;
          }/* else if(cur.WallType == WallID.Dirt) {
            cur.WallType = tumorineWall;
          }*/
          
        }
      }
      
		}
    
    public void PlaceCell(int px, int py, int cellRadius, bool skipAirTiles, List<int> nucleusOrbXs, List<int> nucleusOrbYs) {
      //TODO: random swap between tumorine and cancer stone (and a flag for dat in the functions)
      
      double borderWidth = 1 - (WorldGen.genRand.NextDouble() * 0.125 + 0.15);
      
      for(int yi = -cellRadius; yi <= cellRadius; yi++) {
        for(int xi = -cellRadius; xi <= cellRadius; xi++) {
          double xN = xi / (double)cellRadius;
          double yN = yi / (double)cellRadius;
          double dist = Math.Sqrt(xN * xN + yN * yN);
          if(dist <= 1) {
            if(dist >= borderWidth) {
              if(skipAirTiles) {
                if(Main.tile[px + xi, py + yi].HasTile)
                  Main.tile[px + xi, py + yi].ResetToType(cancerStone);
              } else {
                Main.tile[px + xi, py + yi].ResetToType(cancerStone);
              }
            } else {
              Main.tile[px + xi, py + yi].ClearTile();
            }
            if(dist >= 0.05) {
              if(1 - dist * (WorldGen.genRand.NextDouble() * 0.5 + 0.5) > 0.5) {
                Main.tile[px + xi, py + yi].WallType = cancerStoneWall;
              } else {
                Main.tile[px + xi, py + yi].WallType = tumorineWall;
              }
            }
          }
        }
      }
      
      int nucleusRadius = cellRadius / 8;
      if(nucleusRadius < 1) nucleusRadius = 1;
      bool doOrbNucleus = WorldGen.genRand.Next(4) != 0 && cellRadius < 16;
      
      if(doOrbNucleus) {
        nucleusOrbXs.Add(px + WorldGen.genRand.Next(2));
        nucleusOrbYs.Add(py + WorldGen.genRand.Next(2));
      } else {
        int nx = px + WorldGen.genRand.Next(3) - 1;
        int ny = py + WorldGen.genRand.Next(3) - 1;
        
        for(int yi = -nucleusRadius; yi <= nucleusRadius; yi++) {
          for(int xi = -nucleusRadius; xi <= nucleusRadius; xi++) {
            double xN = xi / (double)nucleusRadius;
            double yN = yi / (double)nucleusRadius;
            double dist = Math.Sqrt(xN * xN + yN * yN);
            if(dist <= 1) {
              Main.tile[nx + xi, ny + yi].ResetToType(tumorine);
            }
          }
        }
      }
    }
    
    public void PlaceTunnels(int px, int py, int radius) {
      //int px = (int)Main.MouseWorld.X / 16;
      //int py = (int)Main.MouseWorld.Y / 16;
      //int radius = WorldGen.genRand.Next(32) + 16;
      int width = radius * 2 + 1;
      int height = radius * 2 + 1;
      
      noiseTable = new byte[width,height];
      
      double ScaleX = 64;//WorldGen.genRand.NextDouble() * 24 + 16;
      double ScaleY = 24;//WorldGen.genRand.NextDouble() * 24 + 16;
      
      for(int yi = 0; yi < height; yi++) {
        for(int xi = 0; xi < width; xi++) {
          noiseTable[xi,yi] = Voronoi(xi / ScaleX + px, yi / ScaleY + py, voronoiSeed);
        }
      }
      //voronoiSeed++;
      
      for(int yi = -radius; yi <= radius; yi++) {
        for(int xi = -radius; xi <= radius; xi++) {
          double xN = xi / (double)radius;
          double yN = yi / (double)radius;
          double dist = Math.Sqrt(xN * xN + yN * yN);
            
          double noisyDist = (1 - dist * dist * dist * dist) * (WorldGen.genRand.NextDouble() * 0.5 + 0.5);
          Tile t = Main.tile[px + xi, py + yi];
          bool willCheck =
            t.HasTile && t.TileType == TileID.Dirt ? dist < 1 : noisyDist > 0.5;
          
          if(willCheck) {
            HashSet<Byte> uniques = new HashSet<Byte>();
            bool hasBorder = false;
            int yBorderRadius = 3;
            int xBorderRadius = WorldGen.genRand.Next(2) + 3;
            for(int yk = -yBorderRadius; yk <= yBorderRadius; yk++) {
              for(int xk = -xBorderRadius; xk <= xBorderRadius; xk++) {
                uniques.Add(noiseTable[iClamp(xk + xi + radius, 0, width - 1), iClamp(yi + yk + radius, 0, height - 1)]);
                hasBorder = uniques.Count > 1;
                if(hasBorder) {
                  break;
                }
              }
              
              if(hasBorder) {
                break;
              }
            }
            
            if(hasBorder) {
              Main.tile[px + xi, py + yi].ClearTile();
            } else {
              Main.tile[px + xi, py + yi].ResetToType(cancerStone);
            }
            Main.tile[px + xi, py + yi].WallType = cancerStoneWall;
            
          }
        }
      }
    }
    public void PlaceCrystal(int px, int py, int radius) {
      int xOffs = -radius;
      
      for(int xc = -radius; xc <= radius; xc++) {
        int height = Math.Abs(radius - Math.Abs(xc + Main.rand.Next(radius / 2)));
        int width = Main.rand.Next(2) + 2;
        int yOffs = (Main.rand.Next(radius) - radius / 2) / 2;
        
        for(int yi = -height; yi <= height; yi++) {
          for(int xi = 0; xi < width; xi++) {
            Tile tile = Main.tile[px + xi + xOffs, py + yi + yOffs];
            
            tile.ResetToType(cancerCrystal);
          }
        }
        xOffs += Main.rand.Next(3) + 1;
      }
    }
    
    public void PlaceAltar(int px, int py) {
      //px and py are the location of the center of the base (as in tiles, not the altar)
      //placement check
      for(int y = 1; y <= 2; y++) {
        for(int x = -1; x <= 1; x++) {
          if(Main.tile[px + x, py - y].TileType == cancerAltar) return;
        }
      }
      
      for(int i = -1; i <= 1; i++) {
        if(!Main.tile[px + i, py].HasTile) Main.tile[px + i, py].ResetToType(cancerStone);
      }
      for(int y = 1; y <= 2; y++) {
        for(int x = -1; x <= 1; x++) {
          Main.tile[px + x, py - y].ClearTile();
        }
      }
      WorldGen.altarCount++;
      WorldGen.PlaceTile(px, py - 1, cancerAltar);
    }
    
    int iClamp(int n, int min, int max) {
      return n > max ? max : (n < min ? min : n);
    }
    
    byte CrudeRandom(int x, int y, int index) {
        return (byte)((x ^ (y * 13 + (x ^ (index * 67))) * 19847323359) * 2643 + ((x * 31) ^ (y * 2911111)));
    }

    byte Voronoi(double x, double y, int index) {
        //TODO: optimize
        int y0 = (int)(y);
        int x0 = (int)(x);

        double shortestDist = double.PositiveInfinity;
        byte closestIndex = 0;

        for(int yi = -1; yi <= 1; yi++) {
            for(int xi = -1; xi <= 1; xi++) {
                //if(xi == 0 && yi == 0) continue;
                double xP = (CrudeRandom(x0 + xi, y0 + yi, index) / 255.0);
                double yP = (CrudeRandom(x0 + xi, y0 + yi, index * 2 + 1) / 255.0);
                double radiusMult = CrudeRandom(x0 + xi, y0 + yi, index * 3 + 2) / 255.0;
                radiusMult = radiusMult * 0.5 + (1 - 0.5);
                //TODO: remove sqrt
                double xSqr = xP + xi + (x0 - x);
                double ySqr = yP + yi + (y0 - y);
                double dist = Math.Sqrt(xSqr * xSqr + ySqr * ySqr) * radiusMult;
                if(dist < shortestDist) {
                    shortestDist = dist;
                    closestIndex = CrudeRandom(x0 + xi, y0 + yi, index * 4 + 3);
                }
            }
        }

        return closestIndex;
    }
	}
}