using Core;
using Core.Pools;
using CoreGameplay.Components;
using CoreGameplay.Components.Collisions;
using CoreGameplay.Components.Tags;
using CoreSystem;
using CoreSystem.Interfaces;
using Extensions;
using GameConfigs.Configs;
using Interfaces.Services;
using Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameConfigs.Factories
{
	[CreateAssetMenu(menuName = "Asteroids/Factories/Ufo")]
	public class UfoFactory : SceneObjectFactory
	{
		[FormerlySerializedAs("Config")]
		[SerializeField]
		private UfoConfig _config;

		// TODO: move key to config
		[SerializeField]
		private string _prefabKey;

		public override void Initialize(AllServices services
			, ComponentsPool componentsPool
			, SceneObjectsPool sceneObjectsPool
			, ICoreWorld coreWorld)
		{
			base.Initialize(services, componentsPool, sceneObjectsPool, coreWorld);
			SceneObjectsPool.RegisterPrefab(_prefabKey, _config.Prefab);
		}

		public override WorldEntity SpawnEntity(Vector2 position = default, Quaternion rotation = default)
		{
			var worldEntity = new WorldEntity(this);

			var entityView = SceneObjectsPool.Get<EntityView>(_prefabKey, position, rotation);
			entityView.WorldEntity = worldEntity;

			var viewLink = ComponentsPool.Get<ViewLink<EntityView>>();
			viewLink.Value = entityView;
			worldEntity.AddComponent(viewLink);

			var enemyTag = ComponentsPool.Get<EnemyTag>();
			worldEntity.AddComponent(enemyTag);

			var movement = ComponentsPool.Get<Movement>();
			movement.Speed = _config.Speed;
			movement.Direction = Vector2.zero;
			worldEntity.AddComponent(movement);

			var transform = ComponentsPool.Get<TransformLink>();
			transform.Transform = entityView.transform;
			worldEntity.AddComponent(transform);

			var health = ComponentsPool.Get<Health>();
			health.Value = 1;
			worldEntity.AddComponent(health);

			var collisionHandler = new CompositeCollisionHandler();
			collisionHandler.AddCollisionHandler(new CollisionWithMissilesHandler());
			collisionHandler.AddCollisionHandler(new ScreenBoundsCheckCollisionHandler(Services.GetSingle<ICameraService>()));

			var collisionBehaviour = ComponentsPool.Get<CollisionBehaviour>();
			collisionBehaviour.CollisionHandler = collisionHandler;
			worldEntity.AddComponent(collisionBehaviour);

			var collisionDetector = ComponentsPool.Get<CollisionDetectorLink>();
			collisionDetector.CollisionDetector = entityView.CollisionDetector;
			worldEntity.AddComponent(collisionDetector);

			WorldEntity playerEntity = null;
			for (var i = 0; i < CoreWorld.AllEntities.Count && playerEntity == null; i++)
			{
				var existingEntity = CoreWorld.AllEntities[i];
				if (existingEntity.HasComponent<PlayerTag>())
				{
					playerEntity = existingEntity;
				}
			}

			if (playerEntity != null && playerEntity.HasComponent<TransformLink>())
			{
				var follow = ComponentsPool.Get<FollowTarget>();
				follow.Transform = playerEntity.GetComponent<TransformLink>().Transform;
				worldEntity.AddComponent(follow);
			}

			return worldEntity;
		}

		public override void DestroyEntity(WorldEntity worldEntity)
		{
			worldEntity.ReturnViewToPool<EntityView>(_prefabKey, SceneObjectsPool);

			base.DestroyEntity(worldEntity);
		}
	}
}
