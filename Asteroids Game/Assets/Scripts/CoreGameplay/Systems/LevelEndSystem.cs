using Core.Systems;
using Core.WorldEntities;
using CoreGameplay.Components.Tags;
using Interfaces.Services;

namespace CoreGameplay.Systems
{
	public class LevelEndSystem : CoreSystem
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
