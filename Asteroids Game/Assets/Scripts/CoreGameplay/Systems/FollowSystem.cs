using CoreGameplay.Components;
using CoreSystem;

namespace CoreGameplay.Systems
{
	public class FollowSystem : CoreSystem.CoreSystem
	{
		public override void Run(float deltaTime)
		{
			foreach (var entity in Entities)
			{
				var movement = entity.GetComponent<Movement>();
				var followTarget = entity.GetComponent<FollowTarget>().Transform;
				var currentTransform = entity.GetComponent<TransformLink>().Transform;
				movement.Direction = (followTarget.position - currentTransform.position).normalized;
			}
		}

		protected override bool Filter(WorldEntity entity)
			=> entity.HasComponent<FollowTarget>() && entity.HasComponent<Movement>() && entity.HasComponent<TransformLink>();
	}
}
