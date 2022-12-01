using System.Collections.Generic;
using Core.WorldEntities;

namespace Core.Systems
{
	public class DeathSystem : CoreSystem
	{
		private readonly CoreLoopRunner _runner;

		public DeathSystem(CoreLoopRunner runner)
		{
			_runner = runner;
		}

		public override void Run(float deltaTime)
		{
			foreach (var entity in Entities)
			{
				if (CheckIfDead(entity))
				{
					if (entity.HasComponent<CustomDeathBehaviour>())
					{
						var deathBehaviour = entity.GetComponent<CustomDeathBehaviour>();
						deathBehaviour.Handler.Handle(entity, _runner);
					}

					_runner.DestroyEntity(entity);
				}
			}
		}

		protected override bool Filter(WorldEntity entity) => entity.HasComponent<Health>();

		private bool CheckIfDead(WorldEntity entity)
		{
			if (entity.GetComponent<Health>().Value > 0)
			{
				return false;
			}

			return true;
		}
	}
}
