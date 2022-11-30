using Core.WorldEntities;
using UnityEngine;

namespace Core.Systems
{
	public class FireSystem : CoreSystem
	{
		public override void Run(float deltaTime)
		{
			foreach (var entity in Entities)
			{
				var shoot = entity.GetComponent<ShootAbility>();

				if (!shoot.Requested || Time.time - shoot.LastActivationTime < shoot.Cooldown)
				{
					continue;
				}

				var transform = entity.GetComponent<TransformLink>().Transform;
				var firePoint = (Vector2)transform.position;

				if (entity.HasComponent<MuzzlePoint>())
				{
					var muzzleOffset = transform.rotation * entity.GetComponent<MuzzlePoint>().Value;
					firePoint += (Vector2)muzzleOffset;
				}

				// TODO: spawn projectile
			}
		}

		protected override bool Filter(WorldEntity entity)
			=> entity.HasComponent<ShootAbility>() && entity.HasComponent<TransformLink>();
	}
}
