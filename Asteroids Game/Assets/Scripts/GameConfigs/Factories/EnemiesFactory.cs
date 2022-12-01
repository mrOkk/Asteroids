using System;
using Core;
using Core.Pools;
using Core.WorldEntities;
using Services;
using UnityEngine;

namespace GameConfigs.Factories
{
	[CreateAssetMenu(menuName = "Asteroids/Factories/Enemies")]
	public class EnemiesFactory : SceneObjectFactory
	{
		[Serializable]
		public class FactoryWithWeight
		{
			public SceneObjectFactory Factory;
			public int Weight;
		}

		public FactoryWithWeight[] EnemyFactories;

		private int _totalWeight;
		private System.Random _randomizer;

		public override void Initialize(AllServices services, ComponentsPool componentsPool, ICoreWorld coreWorld)
		{
			base.Initialize(services, componentsPool, coreWorld);

			_totalWeight = 0;

			foreach (var factoryWithWeight in EnemyFactories)
			{
				_totalWeight += factoryWithWeight.Weight;
			}

			_randomizer = new System.Random();
		}

		public override WorldEntity SpawnEntity(Vector2 position = default, Quaternion rotation = default)
		{
			var weightValue = _randomizer.Next(0, _totalWeight);
			var selectedFactory = EnemyFactories[0].Factory;

			for (var index = 0; index < EnemyFactories.Length && weightValue > 0; index++)
			{
				var factoryWithWeight = EnemyFactories[index];
				selectedFactory = factoryWithWeight.Factory;
				weightValue -= factoryWithWeight.Weight;
			}

			if (selectedFactory == null)
			{
				throw new NullReferenceException("Selected factory is null but it can't be!");
			}

			return selectedFactory.SpawnEntity(position);
		}
	}
}
