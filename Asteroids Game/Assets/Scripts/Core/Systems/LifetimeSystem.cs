using Core.WorldEntities;
using UnityEngine;

namespace Core.Systems
{
	public class LifetimeSystem : CoreSystem
	{
		private readonly CoreLoopRunner _runner;

		public LifetimeSystem(CoreLoopRunner runner)
		{
			_runner = runner;
		}

		public override void Run(float deltaTime)
		{
			foreach (var entity in Entities)
			{
				var lifetime = entity.GetComponent<Lifetime>();

				if (Time.time - lifetime.SpawnTime > lifetime.Value)
				{
					_runner.DestroyEntity(entity);
				}
			}
		}

		protected override bool Filter(WorldEntity entity) => entity.HasComponent<Lifetime>();
	}
}
