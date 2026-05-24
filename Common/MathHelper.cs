using Microsoft.Xna.Framework;
using System;

namespace TheCancerBiome.Common
{
	public class MathHelper
	{
    public static Vector2 Vector2Polar(float theta, float radius) {
      return new Vector2((float)Math.Cos(theta) * radius, (float)Math.Sin(theta) * radius);
    }
	}
}