using UnityEngine;

namespace Configs
{
	[CreateAssetMenu(fileName = "UfoConfig", menuName = "Asteroids/UfoConfig")]
	public class UfoConfig : ScriptableObject
	{
		public GameObject Prefab;
		public float Speed = 1.5f;
	}
}
