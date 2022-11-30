using Configs;
using Core.WorldEntities;
using Interfaces.Services;
using Services;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spawning.Factories
{
	[CreateAssetMenu(menuName = "Asteroids/Factories/Ufo")]
	public class UfoFactory : SceneObjectFactory
	{
		[FormerlySerializedAs("Config")]
		[SerializeField]
		private UfoConfig _config;

		public override WorldEntity SpawnEntity(Vector2 position = default)
		{
			var worldEntity = new WorldEntity(this);

			var prefab = _config.Prefab;
			var entityView = Instantiate(prefab, position, Quaternion.identity);
			entityView.WorldEntity = worldEntity;

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
			throw new System.NotImplementedException();
		}
	}
}
