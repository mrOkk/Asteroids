using Core;
using CoreGameplay.Components;
using CoreSystem;
using CoreSystem.Interfaces;

namespace CoreGameplay.Systems
{
	public class DeathSystem : CoreSystem.CoreSystem
	{
		private readonly ICoreWorld _world;

		public DeathSystem(ICoreWorld world)
		{
			_world = world;
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
						deathBehaviour.Handler.Handle(entity, _world);
					}

					_world.DestroyEntity(entity);
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
