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
	[CreateAssetMenu(menuName = "Asteroids/Factories/Player")]
	public class PlayerFactory : SceneObjectFactory
	{
		[FormerlySerializedAs("Config")]
		[SerializeField]
		private PlayerConfig _config;

		// TODO: move key to config
		[SerializeField]
		private string _prefabKey;

		public override void Initialize(AllServices services
			, ComponentsPool componentsPool
			, SceneObjectsPool sceneObjectsPool
			, ICoreWorld coreWorld)
		{
			base.Initialize(services, componentsPool, sceneObjectsPool, coreWorld);
			sceneObjectsPool.RegisterPrefab(_prefabKey, _config.PlayerPrefab);
		}

		public override WorldEntity SpawnEntity(Vector2 position = default, Quaternion rotation = default)
		{
			var view = SceneObjectsPool.Get<PlayerEntityView>(_prefabKey, position, rotation);
			var entity = new WorldEntity(this);
			view.WorldEntity = entity;

			var viewLink = ComponentsPool.Get<ViewLink<PlayerEntityView>>();
			viewLink.Value = view;
			entity.AddComponent(viewLink);

			var playerTag = ComponentsPool.Get<PlayerTag>();
			entity.AddComponent(playerTag);

			var transform = ComponentsPool.Get<TransformLink>();
			transform.Transform = view.transform;
			entity.AddComponent(transform);

			var acceleration = ComponentsPool.Get<Acceleration>();
			acceleration.Value = 0f;
			entity.AddComponent(acceleration);

			var movement = ComponentsPool.Get<Movement>();
			movement.Speed = 0f;
			movement.Direction = Vector2.zero;
			entity.AddComponent(movement);

			var rotationSpeed = ComponentsPool.Get<RotationSpeed>();
			rotationSpeed.Value = 0f;
			entity.AddComponent(rotationSpeed);

			var health = ComponentsPool.Get<Health>();
			health.Value = 1;
			entity.AddComponent(health);

			var muzzlePoint = ComponentsPool.Get<MuzzlePoint>();
			muzzlePoint.Value = view.MuzzlePointTransform.localPosition;
			entity.AddComponent(muzzlePoint);

			var shootAbility = ComponentsPool.Get<ShootAbility>();
			shootAbility.Cooldown = _config.ShootCooldown;
			shootAbility.Requested = false;
			shootAbility.LastActivationTime = 0f;
			shootAbility.ShootFactory = _config.MissileFactory;
			entity.AddComponent(shootAbility);

			var alternativeShootAbility = ComponentsPool.Get<AlternativeShootAbility>();
			alternativeShootAbility.LastActivationTime = 0f;
			alternativeShootAbility.Count = _config.LaserCount;
			alternativeShootAbility.MaxCount = _config.LaserCount;
			alternativeShootAbility.Cooldown = _config.LaserCooldown;
			alternativeShootAbility.LastActivationTime = 0f;
			alternativeShootAbility.RestoreTime = _config.LaserRestoreTime;
			alternativeShootAbility.ShootFactory = _config.LaserFactory;
			entity.AddComponent(alternativeShootAbility);

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
			worldEntity.ReturnViewToPool<PlayerEntityView>(_prefabKey, SceneObjectsPool);

			base.DestroyEntity(worldEntity);
		}
	}
}
