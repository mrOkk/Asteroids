using Core.WorldEntities;
using Spawning.Factories;
using UnityEngine;

namespace Core.Systems
{
	public class AlternativeFireSystem : CoreSystem
	{
		private readonly CoreLoopRunner _runner;

		public AlternativeFireSystem(CoreLoopRunner runner)
		{
			_runner = runner;
		}

		public override void Run(float deltaTime)
		{
			foreach (var entity in Entities)
			{
				var alternativeShoot = entity.GetComponent<AlternativeShootAbility>();
				Restore(alternativeShoot);
				Shoot(entity, alternativeShoot);
			}
		}

		protected override bool Filter(WorldEntity entity)
			=> entity.HasComponent<AlternativeShootAbility>() && entity.HasComponent<TransformLink>();

		private void Restore(AlternativeShootAbility alternativeShoot)
		{
			if (alternativeShoot.Count >= alternativeShoot.MaxCount || alternativeShoot.Count < 0)
			{
				return;
			}

			if (Time.time - alternativeShoot.LastRestoredTime < alternativeShoot.RestoreTime)
			{
				return;
			}

			alternativeShoot.Count++;
			alternativeShoot.LastRestoredTime = Time.time;
		}

		private void Shoot(WorldEntity entity, AlternativeShootAbility alternativeShoot)
		{
			if (!alternativeShoot.Requested || alternativeShoot.Count == 0 || Time.time - alternativeShoot.LastActivationTime < alternativeShoot.Cooldown)
			{
				return;
			}

			var transform = entity.GetComponent<TransformLink>().Transform;
			var firePoint = (Vector2)transform.position;

			if (entity.HasComponent<MuzzlePoint>())
			{
				var muzzleOffset = transform.rotation * entity.GetComponent<MuzzlePoint>().Value;
				firePoint += (Vector2)muzzleOffset;
			}

			var shootEntity = alternativeShoot.ShootFactory.SpawnEntity(firePoint, transform.rotation);
			_runner.AddEntity(shootEntity);

			if (alternativeShoot.Count == alternativeShoot.MaxCount)
			{
				alternativeShoot.LastRestoredTime = Time.time;
			}

			alternativeShoot.Count--;
		}
	}
}
