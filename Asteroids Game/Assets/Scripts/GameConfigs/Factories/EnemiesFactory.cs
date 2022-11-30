using System;
using Core.WorldEntities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawning.Factories
{
	[CreateAssetMenu(menuName = "Asteroids/Factories/Enemies")]
	public class EnemiesFactory : SceneObjectFactory
	{
		[Serializable]
		public class FactoryWithWeight
		{
			public SceneObjectFactory Factory;
			public float Weight;
		}

		public FactoryWithWeight[] EnemyFactories;

		private float _totalWeight;

		public override WorldEntity SpawnEntity(Vector2 position = default)
		{
			var totalWeight = GetTotalWeight();
			var weightValue = Random.Range(0f, totalWeight);

			SceneObjectFactory selectedFactory = null;

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

		public override void DestroyEntity(WorldEntity worldEntity)
		{
			throw new NotImplementedException();
		}

		private float GetTotalWeight()
		{
			if (_totalWeight <= float.Epsilon)
			{
				_totalWeight = 0;

				foreach (var factoryWithWeight in EnemyFactories)
				{
					_totalWeight += factoryWithWeight.Weight;
				}
			}

			return _totalWeight;
		}
	}
}
