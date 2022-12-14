using CoreGameplay.Components.Tags;
using CoreSystem;
using Interfaces.Services;

namespace CoreGameplay.Systems
{
	public class LevelEndSystem : CoreSystem.CoreSystem
	{
		private readonly ILevel _level;

		public LevelEndSystem(ILevel level)
		{
			_level = level;
		}

		public override void RemoveEntity(WorldEntity entity)
		{
			base.RemoveEntity(entity);

			if (entity.HasComponent<PlayerTag>())
			{
				_level.End();
			}
		}

		public override void Run(float deltaTime)
		{
		}

		protected override bool Filter(WorldEntity entity) => false;
	}
}
