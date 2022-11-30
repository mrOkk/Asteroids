using Configs;
using Core;
using Core.Pools;
using Core.WorldEntities;
using Interfaces.Services;
using Services;
using UnityEngine;

namespace Spawning.Factories
{
	[CreateAssetMenu(menuName = "Asteroids/Factories/Asteroids")]
	public class AsteroidsFactory : SceneObjectFactory
	{
		[SerializeField]
		private AsteroidsConfig _config;

		private SceneObjectsPool<EntityView> _viewsPool;

		public override void Initialize(AllServices services, ComponentsPool componentsPool)
		{
			base.Initialize(services, componentsPool);

			_viewsPool = new SceneObjectsPool<EntityView>(_config.Prefab);
		}

		public override WorldEntity SpawnEntity(Vector2 position = default)
		{
			var worldEntity = new WorldEntity(this);

			var entityView = _viewsPool.Get(position);
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

			var rotation = ComponentsPool.Get<RotationSpeed>();
			rotation.Value = rotationAngle;
			worldEntity.AddComponent(rotation);

			var transform = ComponentsPool.Get<TransformLink>();
			transform.Transform = entityView.transform;
			worldEntity.AddComponent(transform);

			var health = ComponentsPool.Get<Health>();
			health.Value = 1;
			worldEntity.AddComponent(health);

			var collisionHandler = new CompositeCollisionHandler();
			collisionHandler.AddCollisionHandler(new PlayerCollisionHandler());
			collisionHandler.AddCollisionHandler(new ScreenBoundsCheckCollisionHandler(AllServices.Container.GetSingle<ICameraService>()));

			var collisionBehaviour = ComponentsPool.Get<CollisionBehaviour>();
			collisionBehaviour.CollisionHandler = collisionHandler;

			worldEntity.AddComponent(collisionBehaviour);

			var collisionDetector = ComponentsPool.Get<CollisionDetectorLink>();
			collisionDetector.CollisionDetector = entityView.CollisionDetector;
			worldEntity.AddComponent(collisionDetector);

			return worldEntity;
		}

		public override void DestroyEntity(WorldEntity worldEntity)
		{
			var viewLink = worldEntity.GetComponent<ViewLink<EntityView>>();
			_viewsPool.Return(viewLink.Value);
			viewLink.Value = null;

			base.DestroyEntity(worldEntity);
		}
	}
}
