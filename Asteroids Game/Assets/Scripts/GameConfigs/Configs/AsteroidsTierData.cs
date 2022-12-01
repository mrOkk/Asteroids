using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Configs
{
	[Serializable]
	public class AsteroidsTierData
	{
		public uint Tier;
		public List<EntityView> Prefab = new();
	}
}
