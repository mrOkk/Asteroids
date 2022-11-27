using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
	[Serializable]
	public class AsteroidsTierData
	{
		public uint Tier;
		public List<GameObject> Prefab = new();
		public float Speed = 1f;
	}
}
