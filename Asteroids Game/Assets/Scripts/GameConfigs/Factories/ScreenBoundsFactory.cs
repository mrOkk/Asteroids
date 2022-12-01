using Core;
using Core.Pools;
using Core.WorldEntities;
using Interfaces.Services;
using Services;
using UnityEngine;

namespace Spawning.Factories
{
	[CreateAssetMenu(fileName = "ScreenBoundsFactory", menuName = "Asteroids/Factories/Screen bounds")]
	public class ScreenBoundsFactory : SceneObjectFactory
	{
		[SerializeField]
		private ScreenFieldView _screenBoundsPrefab;

		private ICameraService _cameraService;
		private SceneObjectsPool<ScreenFieldView> _sceneObjectsPool;

		public override void Initialize(AllServices services, ComponentsPool componentsPool)
		{
			base.Initialize(services, componentsPool);
			_cameraService = services.GetSingle<ICameraService>();
			_sceneObjectsPool = new SceneObjectsPool<ScreenFieldView>(_screenBoundsPrefab);
		}

		public override WorldEntity SpawnEntity(Vector2 position = default)
		{
			var boundsEntity = new WorldEntity(this);
			var boundsView = _sceneObjectsPool.Get(_cameraService.MainCamera.transform.position);
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
			var viewLink = worldEntity.GetComponent<ViewLink<ScreenFieldView>>();
			_sceneObjectsPool.Return(viewLink.Value);
			viewLink.Value = null;

			base.DestroyEntity(worldEntity);
		}
	}
}
