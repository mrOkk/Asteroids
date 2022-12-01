using Core;
using GameConfigs.Factories;
using UnityEngine;

namespace GameConfigs.Configs
{
	[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Asteroids/PlayerConfig")]
	public class PlayerConfig : ScriptableObject
	{
		public MissileFactory MissileFactory;
		public LaserFactory LaserFactory;

		public PlayerEntityView PlayerPrefab;
		[Range(0.1f, 10f)]
		public float Acceleration = 1f;
		public float RotationSpeed = 300;
		public float ShootCooldown = 1f;
		public int LaserCount = 1;
		public float LaserCooldown = 1f;
		public float LaserRestoreTime = 3f;
	}
}
