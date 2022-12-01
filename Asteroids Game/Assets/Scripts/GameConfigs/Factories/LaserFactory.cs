using Core;
using Core.Pools;
using CoreGameplay.Components;
using CoreGameplay.Components.Tags;
using CoreSystem;
using CoreSystem.Interfaces;
using Services;
using UnityEngine;

namespace GameConfigs.Factories
{
	[CreateAssetMenu(menuName = "Asteroids/Factories/Laser")]
	public class LaserFactory : SceneObjectFactory
	{
		[SerializeField]
		private EntityView _prefab;
		[SerializeField]
		private float _lifetime = 0.1f;
		[SerializeField]
		private float _length = 10f;

		private SceneObjectsPool<EntityView> _sceneObjectsPool;


		public override void Initialize(AllServices services
			, ComponentsPool componentsPool
			, ICoreWorld coreWorld)
		{
			base.Initialize(services, componentsPool, coreWorld);
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

			var transformLink = ComponentsPool.Get<TransformLink>();
			transformLink.Transform = view.transform;
			entity.AddComponent(transformLink);

			var laser = ComponentsPool.Get<Laser>();
			laser.Handled = false;
			laser.Length = _length;
			entity.AddComponent(laser);

			var lifetime = ComponentsPool.Get<Lifetime>();
			lifetime.Value = _lifetime;
			lifetime.SpawnTime = Time.time;
			entity.AddComponent(lifetime);

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
