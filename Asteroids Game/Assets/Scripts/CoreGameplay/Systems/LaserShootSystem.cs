using Core;
using Core.Systems;
using Core.WorldEntities;
using CoreGameplay.Components;
using UnityEngine;

namespace CoreGameplay.Systems
{
	public class LaserShootSystem : CoreSystem
	{
		private RaycastHit2D[] _hits = new RaycastHit2D[20];

		public override void Run(float deltaTime)
		{
			foreach (var entity in Entities)
			{
				var laser = entity.GetComponent<Laser>();

				if (laser.Handled)
				{
					continue;
				}

				var transform = entity.GetComponent<TransformLink>().Transform;
				var hitsCount =
					Physics2D.RaycastNonAlloc(transform.position, transform.up, _hits);

				for (var i = 0; i < hitsCount; i++)
				{
					var hit = _hits[i];
					var entityView = hit.transform.GetComponent<EntityView>();

					if (entityView != null)
					{
						laser.OnHitDetected?.Invoke(entityView.WorldEntity, entity);
						laser.OnHitDetected?.Invoke(entity, entityView.WorldEntity);
					}
				}

				laser.Handled = true;
			}
		}

		protected override bool Filter(WorldEntity entity)
			=> entity.HasComponent<Laser>() && entity.HasComponent<TransformLink>();
	}
}
