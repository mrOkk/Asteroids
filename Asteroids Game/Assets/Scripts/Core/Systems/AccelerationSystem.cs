using Core.WorldEntities;
using UnityEngine;

namespace Core.Systems
{
	public class AccelerationSystem : CoreSystem
	{
		public override void Run(float deltaTime)
		{
			foreach (var entity in Entities)
			{
				var acceleration = entity.GetComponent<Acceleration>();
				var movement = entity.GetComponent<Movement>();
				var transform = entity.GetComponent<TransformLink>().Transform;

				var vectorSpeed = movement.Speed * movement.Direction;
				vectorSpeed += (Vector2)(transform.up * acceleration.Value * deltaTime);

				movement.Speed = vectorSpeed.magnitude;
				movement.Direction = Mathf.Approximately(movement.Speed, 0)
					? Vector2.zero
					: vectorSpeed / movement.Speed;
			}
		}

		protected override bool Filter(WorldEntity entity)
			=> entity.HasComponent<Acceleration>() && entity.HasComponent<Movement>() && entity.HasComponent<TransformLink>();
	}
}
