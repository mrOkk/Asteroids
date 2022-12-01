using Core;
using Core.Systems;
using Core.WorldEntities;
using CoreGameplay.Components;
using UnityEngine;

namespace CoreGameplay.Systems
{
	public class FireSystem : CoreSystem
	{
		private readonly ICoreWorld _world;

		public FireSystem(ICoreWorld world)
		{
			_world = world;
		}

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

				var missileEntity = shoot.ShootFactory.SpawnEntity(firePoint, transform.rotation);

				if (missileEntity.HasComponent<Movement>())
				{
					var missileMovement = missileEntity.GetComponent<Movement>();
					missileMovement.Direction = transform.up;
				}

				_world.AddEntity(missileEntity);

				shoot.LastActivationTime = Time.time;
			}
		}

		protected override bool Filter(WorldEntity entity)
			=> entity.HasComponent<ShootAbility>() && entity.HasComponent<TransformLink>();
	}
}
