using Extensions;
using Spawning.Factories;
using UnityEngine;

namespace Core.WorldEntities.DeathBehaviour
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

		public void Handle(WorldEntity owner, CoreLoopRunner runner)
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

				runner.AddEntity(entity);
			}
		}
	}
}
