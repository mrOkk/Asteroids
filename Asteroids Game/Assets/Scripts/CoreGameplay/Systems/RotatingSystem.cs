using Core.Systems;
using Core.WorldEntities;
using CoreGameplay.Components;

namespace CoreGameplay.Systems
{
	public class RotatingSystem : CoreSystem
	{
		public override void Run(float deltaTime)
		{
			foreach (var entity in Entities)
			{
				var transformLink = entity.GetComponent<TransformLink>();
				var rotation = entity.GetComponent<RotationSpeed>();

				transformLink.Transform.Rotate(0, 0, rotation.Value * deltaTime);
			}
		}

		protected override bool Filter(WorldEntity entity)
			=> entity.HasComponent<RotationSpeed>() && entity.HasComponent<TransformLink>();
	}
}
