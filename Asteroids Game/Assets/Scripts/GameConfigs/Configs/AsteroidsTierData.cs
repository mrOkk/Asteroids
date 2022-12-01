using System;
using System.Collections.Generic;
using Core;

namespace GameConfigs.Configs
{
	[Serializable]
	public class AsteroidsTierData
	{
		public uint Tier;
		public List<EntityView> Prefab = new();
	}
}
