using Core;
using Core.Pools;
using CoreGameplay.Components;
using CoreGameplay.Components.Collisions;
using CoreGameplay.Components.DeathBehaviour;
using CoreGameplay.Components.Tags;
using CoreSystem;
using CoreSystem.Interfaces;
using Extensions;
using GameConfigs.Configs;
using Interfaces.Services;
using Services;
using UnityEngine;

namespace GameConfigs.Factories
{
	[CreateAssetMenu(menuName = "Asteroids/Factories/Asteroids")]
	public class AsteroidsFactory : SceneObjectFactory
	{
		[SerializeField]
		private AsteroidsConfig _config;

		private SceneObjectsPool<EntityView> _viewsPool;

		public override void Initialize(
			AllServices services, ComponentsPool componentsPool, ICoreWorld coreWorld)
		{
			base.Initialize(services, componentsPool, coreWorld);

			_viewsPool = new SceneObjectsPool<EntityView>(_config.Prefab);
		}

		public override WorldEntity SpawnEntity(Vector2 position = default, Quaternion rotation = default)
		{
			var worldEntity = new WorldEntity(this);

			var entityView = _viewsPool.Get(position, rotation);
			entityView.WorldEntity = worldEntity;

			var moveSpeed = _config.Speed;
			var rotationAngle = (Random.Range(0, 2) == 1 ? 1f : -1f) * Random.Range(0, _config.MaxRotationSpeed);

			var viewLink = ComponentsPool.Get<ViewLink<EntityView>>();
			viewLink.Value = entityView;
			worldEntity.AddComponent(viewLink);

			worldEntity.AddComponent(ComponentsPool.Get<EnemyTag>());

			var movement = ComponentsPool.Get<Movement>();
			movement.Speed = moveSpeed;
			worldEntity.AddComponent(movement);

			var rotationSpeed = ComponentsPool.Get<RotationSpeed>();
			rotationSpeed.Value = rotationAngle;
			worldEntity.AddComponent(rotationSpeed);

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

			if (_config.NextTierFactory != null || _config.NextTierCountToSpawn > 0)
			{
				var deathBehaviour = ComponentsPool.Get<CustomDeathBehaviour>();
				deathBehaviour.Handler =
					new SpawnAsteroidsPartsOnDeath(_config.NextTierFactory, _config.NextTierCountToSpawn);
				worldEntity.AddComponent(deathBehaviour);
			}

			return worldEntity;
		}

		public override void DestroyEntity(WorldEntity worldEntity)
		{
			worldEntity.ReturnViewToPool(_viewsPool);

			base.DestroyEntity(worldEntity);
		}
	}
}
