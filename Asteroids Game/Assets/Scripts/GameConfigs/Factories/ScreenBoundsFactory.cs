using Core;
using Core.Pools;
using CoreGameplay.Components;
using CoreGameplay.Components.Tags;
using CoreSystem;
using CoreSystem.Interfaces;
using Extensions;
using Interfaces.Services;
using Services;
using UnityEngine;

namespace GameConfigs.Factories
{
	[CreateAssetMenu(fileName = "ScreenBoundsFactory", menuName = "Asteroids/Factories/Screen bounds")]
	public class ScreenBoundsFactory : SceneObjectFactory
	{
		// TODO: move to config
		[SerializeField]
		private ScreenFieldView _screenBoundsPrefab;

		// TODO: move key to config
		[SerializeField]
		private string _prefabKey;

		private ICameraService _cameraService;

		public override void Initialize(AllServices services
			, ComponentsPool componentsPool
			, SceneObjectsPool sceneObjectsPool
			, ICoreWorld coreWorld)
		{
			base.Initialize(services, componentsPool, sceneObjectsPool, coreWorld);
			_cameraService = services.GetSingle<ICameraService>();
			SceneObjectsPool.RegisterPrefab(_prefabKey, _screenBoundsPrefab);
		}

		public override WorldEntity SpawnEntity(Vector2 position = default, Quaternion rotation = default)
		{
			var boundsEntity = new WorldEntity(this);
			var cameraTransform = _cameraService.MainCamera.transform;
			var boundsView = SceneObjectsPool.Get<ScreenFieldView>(_prefabKey, cameraTransform.position, cameraTransform.rotation);
			boundsView.WorldEntity = boundsEntity;
			boundsView.BoxCollider.size = _cameraService.WorldScreenSize;

			var viewLink = ComponentsPool.Get<ViewLink<ScreenFieldView>>();
			viewLink.Value = boundsView;
			boundsEntity.AddComponent(viewLink);

			var tagComponent = ComponentsPool.Get<ScreenBoundsTag>();
			boundsEntity.AddComponent(tagComponent);

			return boundsEntity;
		}

		public override void DestroyEntity(WorldEntity worldEntity)
		{
			worldEntity.ReturnViewToPool<ScreenFieldView>(_prefabKey, SceneObjectsPool);

			base.DestroyEntity(worldEntity);
		}
	}
}
