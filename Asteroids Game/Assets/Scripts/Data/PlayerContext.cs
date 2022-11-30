using UnityEngine;

namespace Data
{
	public class PlayerContext
	{
		public int Score;
		public int LasersCount;
		public int LasersMaxCount;
		public float LaserUseTime;
		public float LaserCooldownTime;
		public float CurrentSpeed;
		public Vector2 Coordinates;
		public float RotationAngle;
		public bool IsAlive;
	}
}
