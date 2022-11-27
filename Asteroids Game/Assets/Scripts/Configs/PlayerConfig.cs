using UnityEngine;

namespace Configs
{
	[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Asteroids/PlayerConfig")]
	public class PlayerConfig : ScriptableObject
	{
		public GameObject PlayerPrefab;
		[Range(0.1f, 10f)]
		public float Acceleration = 1f;
		public int LaserCount = 1;
		public float LaserCooldown = 1f;
		public float LaserRestoreTime = 3f;
	}
}
