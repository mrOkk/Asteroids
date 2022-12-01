using UnityEngine;

namespace Extensions
{
	public static class Vector2Extension
	{
		public static Vector2 Rotate(this Vector2 vector, float degrees)
		{
			var sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
			var cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

			var tx = vector.x;
			var ty = vector.y;
			vector.x = (cos * tx) - (sin * ty);
			vector.y = (sin * tx) + (cos * ty);
			return vector;
		}
	}
}
