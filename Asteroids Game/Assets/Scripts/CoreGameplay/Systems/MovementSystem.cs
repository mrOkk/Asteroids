using System.Collections.Generic;
using CoreGameplay.Components;
using CoreSystem;
using UnityEngine;

namespace CoreGameplay.Systems
{
	public class MovementSystem : CoreSystem.CoreSystem
	{
		private List<WorldEntity> _filter;

		public override void Run(float deltaTime)
		{
			foreach (var entity in Entities)
			{
				var transformLink = entity.GetComponent<TransformLink>();
				var movement = entity.GetComponent<Movement>();
				transformLink.Transform.position += (Vector3)(movement.Direction * movement.Speed * deltaTime);
			}
		}

		protected override bool Filter(WorldEntity entity)
			=> entity.HasComponent<TransformLink>() && entity.HasComponent<Movement>();
	}
}
