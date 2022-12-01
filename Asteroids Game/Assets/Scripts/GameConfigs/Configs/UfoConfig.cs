using Core;
using UnityEngine;

namespace Configs
{
	[CreateAssetMenu(fileName = "UfoConfig", menuName = "Asteroids/UfoConfig")]
	public class UfoConfig : ScriptableObject
	{
		public EntityView Prefab;
		public float Speed = 1.5f;
		public float SpawnWeight = 1;
	}
}
