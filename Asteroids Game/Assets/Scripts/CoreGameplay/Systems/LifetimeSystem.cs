using Core;
using Core.Systems;
using Core.WorldEntities;
using CoreGameplay.Components;
using UnityEngine;

namespace CoreGameplay.Systems
{
	public class LifetimeSystem : CoreSystem
	{
		private readonly ICoreWorld _world;

		public LifetimeSystem(ICoreWorld world)
		{
			_world = world;
		}

		public override void Run(float deltaTime)
		{
			foreach (var entity in Entities)
			{
				var lifetime = entity.GetComponent<Lifetime>();

				if (Time.time - lifetime.SpawnTime > lifetime.Value)
				{
					_world.DestroyEntity(entity);
				}
			}
		}

		protected override bool Filter(WorldEntity entity) => entity.HasComponent<Lifetime>();
	}
}
