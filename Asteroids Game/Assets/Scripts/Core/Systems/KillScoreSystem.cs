using Core.WorldEntities;
using Interfaces.UIContexts;

namespace Core.Systems
{
	public class KillScoreSystem : CoreSystem
	{
		private readonly ICoreContext _coreContext;

		public KillScoreSystem(ICoreContext coreContext)
		{
			_coreContext = coreContext;
		}

		public override void RemoveEntity(WorldEntity entity)
		{
			base.RemoveEntity(entity);

			if (entity.HasComponent<EnemyTag>())
			{
				_coreContext.KillScore++;
			}
		}

		public override void Run(float deltaTime)
		{
		}

		protected override bool Filter(WorldEntity entity) => false;
	}
}
