using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
	[CreateAssetMenu(fileName = "AsteroidsConfig", menuName = "Asteroids/AsteroidsConfig")]
	public class AsteroidsConfig : ScriptableObject
	{
		public List<AsteroidsTierData> AsteroidsTiers = new();
	}
}
