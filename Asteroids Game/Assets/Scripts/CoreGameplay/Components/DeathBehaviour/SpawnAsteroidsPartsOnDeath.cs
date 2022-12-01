using Core;
using CoreSystem;
using CoreSystem.Interfaces;
using Extensions;
using GameConfigs.Factories;
using UnityEngine;

namespace CoreGameplay.Components.DeathBehaviour
{
	public class SpawnAsteroidsPartsOnDeath : IDeathHandler
	{
		private readonly SceneObjectFactory _factory;
		private readonly int _count;

		public SpawnAsteroidsPartsOnDeath(SceneObjectFactory factory, int count)
		{
			_factory = factory;
			_count = count;
		}

		public void Handle(WorldEntity owner, ICoreWorld world)
		{
			if (!owner.HasComponent<TransformLink>() || !owner.HasComponent<Movement>())
			{
				return;
			}

			var transform = owner.GetComponent<TransformLink>().Transform;
			var ownerMovement = owner.GetComponent<Movement>();

			for (var i = 0; i < _count; i++)
			{
				var entity = _factory.SpawnEntity(transform.position);

				if (entity.HasComponent<Movement>())
				{
					var entityMovement = entity.GetComponent<Movement>();
					entityMovement.Direction = ownerMovement.Direction.Rotate(Random.Range(0, 360)).normalized;
				}

				world.AddEntity(entity);
			}
		}
	}
}
