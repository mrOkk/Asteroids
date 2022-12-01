using Core.WorldEntities;
using UnityEngine;

namespace Core.Systems
{
	public class FireSystem : CoreSystem
	{
		private readonly CoreLoopRunner _runner;

		public FireSystem(CoreLoopRunner runner)
		{
			_runner = runner;
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

				_runner.AddEntity(missileEntity);

				shoot.LastActivationTime = Time.time;
			}
		}

		protected override bool Filter(WorldEntity entity)
			=> entity.HasComponent<ShootAbility>() && entity.HasComponent<TransformLink>();
	}
}
