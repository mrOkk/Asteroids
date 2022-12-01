using Core.WorldEntities;
using Interfaces.Services;

namespace Core.Systems
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
