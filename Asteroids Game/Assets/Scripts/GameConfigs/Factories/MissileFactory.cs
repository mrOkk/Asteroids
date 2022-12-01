using Core;
using Core.Pools;
using Core.WorldEntities;
using Interfaces.Services;
using Services;
using UnityEngine;

namespace Spawning.Factories
{
	[CreateAssetMenu(menuName = "Asteroids/Factories/Missile")]
	public class MissileFactory : SceneObjectFactory
	{
		[SerializeField]
		private EntityView _prefab;
		[SerializeField]
		private float _speed = 7f;
		[SerializeField]
		private float _lifetime = 1f;

		private SceneObjectsPool<EntityView> _sceneObjectsPool;

		public override void Initialize(AllServices services, ComponentsPool componentsPool)
		{
			base.Initialize(services, componentsPool);
			_sceneObjectsPool = new SceneObjectsPool<EntityView>(_prefab);
		}

		public override WorldEntity SpawnEntity(Vector2 position = default, Quaternion rotation = default)
		{
			var entity = new WorldEntity(this);
			var view = _sceneObjectsPool.Get(position, rotation);
			view.WorldEntity = entity;

			var viewLink = ComponentsPool.Get<ViewLink<EntityView>>();
			viewLink.Value = view;
			entity.AddComponent(viewLink);

			var missileTag = ComponentsPool.Get<MissileTag>();
			entity.AddComponent(missileTag);

			var movement = ComponentsPool.Get<Movement>();
			movement.Speed = _speed;
			movement.Direction = Vector2.zero;
			entity.AddComponent(movement);

			var transformLink = ComponentsPool.Get<TransformLink>();
			transformLink.Transform = view.transform;
			entity.AddComponent(transformLink);

			var health = ComponentsPool.Get<Health>();
			health.Value = 1;
			entity.AddComponent(health);

			var lifetime = ComponentsPool.Get<Lifetime>();
			lifetime.SpawnTime = Time.time;
			lifetime.Value = _lifetime;
			entity.AddComponent(lifetime);

			var collisionHandler = new CompositeCollisionHandler();
			collisionHandler.AddCollisionHandler(new CollisionWithEnemiesHandler());
			collisionHandler.AddCollisionHandler(new ScreenBoundsCheckCollisionHandler(AllServices.Container.GetSingle<ICameraService>()));

			var collisionBehaviour = ComponentsPool.Get<CollisionBehaviour>();
			collisionBehaviour.CollisionHandler = collisionHandler;
			entity.AddComponent(collisionBehaviour);


			var collisionDetector = ComponentsPool.Get<CollisionDetectorLink>();
			collisionDetector.CollisionDetector = view.CollisionDetector;
			entity.AddComponent(collisionDetector);

			return entity;
		}

		public override void DestroyEntity(WorldEntity worldEntity)
		{
			var viewLink = worldEntity.GetComponent<ViewLink<EntityView>>();
			_sceneObjectsPool.Return(viewLink.Value);
			viewLink.Value = null;

			base.DestroyEntity(worldEntity);
		}
	}
}
