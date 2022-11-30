using System.Collections.Generic;
using Core.WorldEntities;

namespace Core.Systems
{
	public class DeathSystem : CoreSystem
	{
		private readonly CoreLoopRunner _runner;
		private readonly List<WorldEntity> _deadEntities = new(3);

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
					_deadEntities.Add(entity);
				}
			}

			if (_deadEntities.Count <= 0)
			{
				return;
			}

			foreach (var entity in _deadEntities)
			{
				_runner.DestroyEntity(entity);
			}

			_deadEntities.Clear();
		}

		protected override bool Filter(WorldEntity entity) => entity.HasComponent<Health>();

		private bool CheckIfDead(WorldEntity entity)
		{
			if (entity.GetComponent<Health>().Value > 0)
			{
				return false;
			}

			if (entity.HasComponent<CustomDeathBehaviour>())
			{
				var onDeathSpawn = entity.GetComponent<CustomDeathBehaviour>();

				for (var i = 0; i < onDeathSpawn.SpawnCount; i++)
				{
					var newEntity = onDeathSpawn.Factory.SpawnEntity();
					_runner.AddEntity(newEntity);
				}
			}

			return true;
		}
	}
}
