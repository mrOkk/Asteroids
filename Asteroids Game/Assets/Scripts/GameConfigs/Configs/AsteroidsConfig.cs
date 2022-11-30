using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Configs
{
	[CreateAssetMenu(fileName = "AsteroidsConfig", menuName = "Asteroids/AsteroidsConfig")]
	public class AsteroidsConfig : ScriptableObject
	{
		public EntityView Prefab;
		public float Speed = 1f;
		public float MaxRotationSpeed = 30f;
		public AsteroidsConfig NextTierConfig;
	}
}
