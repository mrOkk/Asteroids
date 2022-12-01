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
		[SerializeField]
		private ScreenFieldView _screenBoundsPrefab;

		private ICameraService _cameraService;
		private SceneObjectsPool<ScreenFieldView> _sceneObjectsPool;

		public override void Initialize(
			AllServices services, ComponentsPool componentsPool, ICoreWorld coreWorld)
		{
			base.Initialize(services, componentsPool, coreWorld);
			_cameraService = services.GetSingle<ICameraService>();
			_sceneObjectsPool = new SceneObjectsPool<ScreenFieldView>(_screenBoundsPrefab);
		}

		public override WorldEntity SpawnEntity(Vector2 position = default, Quaternion rotation = default)
		{
			var boundsEntity = new WorldEntity(this);
			var cameraTransform = _cameraService.MainCamera.transform;
			var boundsView = _sceneObjectsPool.Get(cameraTransform.position, cameraTransform.rotation);
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
			worldEntity.ReturnViewToPool(_sceneObjectsPool);

			base.DestroyEntity(worldEntity);
		}
	}
}
