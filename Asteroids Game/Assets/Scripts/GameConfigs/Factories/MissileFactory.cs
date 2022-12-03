using Core;
using Core.Pools;
using CoreGameplay.Components;
using CoreGameplay.Components.Collisions;
using CoreGameplay.Components.Tags;
using CoreSystem;
using CoreSystem.Interfaces;
using Extensions;
using Interfaces.Services;
using Services;
using UnityEngine;

namespace GameConfigs.Factories
{
	[CreateAssetMenu(menuName = "Asteroids/Factories/Missile")]
	public class MissileFactory : SceneObjectFactory
	{
		// TODO: convert everything to config
		[SerializeField]
		private EntityView _prefab;
		[SerializeField]
		private float _speed = 7f;
		[SerializeField]
		private float _lifetime = 1f;

		// TODO: move key to config
		[SerializeField]
		private string _prefabKey;

		public override void Initialize(AllServices services
			, ComponentsPool componentsPool
			, SceneObjectsPool sceneObjectsPool
			, ICoreWorld coreWorld)
		{
			base.Initialize(services, componentsPool, sceneObjectsPool, coreWorld);
			SceneObjectsPool.RegisterPrefab(_prefabKey, _prefab);
		}

		public override WorldEntity SpawnEntity(Vector2 position = default, Quaternion rotation = default)
		{
			var entity = new WorldEntity(this);
			var view = SceneObjectsPool.Get<EntityView>(_prefabKey, position, rotation);
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
			collisionHandler.AddCollisionHandler(new ScreenBoundsCheckCollisionHandler(Services.GetSingle<ICameraService>()));

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
			worldEntity.ReturnViewToPool<EntityView>(_prefabKey, SceneObjectsPool);

			base.DestroyEntity(worldEntity);
		}
	}
}
