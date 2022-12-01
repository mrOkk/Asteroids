using Core;
using Spawning.Factories;
using UnityEngine;

namespace Configs
{
	[CreateAssetMenu(fileName = "AsteroidsConfig", menuName = "Asteroids/AsteroidsConfig")]
	public class AsteroidsConfig : ScriptableObject
	{
		public EntityView Prefab;
		public float Speed = 1f;
		public float MaxRotationSpeed = 30f;
		public int NextTierCountToSpawn = 2;
		public AsteroidsFactory NextTierFactory;
	}
}
