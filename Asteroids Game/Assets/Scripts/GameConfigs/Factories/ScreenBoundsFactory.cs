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

		public override void Initialize(AllServices services, ComponentsPool componentsPool)
		{
			base.Initialize(services, componentsPool);
			_cameraService = services.GetSingle<ICameraService>();
		}

		public override WorldEntity SpawnEntity(Vector2 position = default)
		{
			var boundsEntity = new WorldEntity(this);
			var boundsView = Instantiate(_screenBoundsPrefab, _cameraService.MainCamera.transform.position, Quaternion.identity);
			boundsView.WorldEntity = boundsEntity;
			boundsView.BoxCollider.size = _cameraService.WorldScreenSize;

			boundsEntity.AddComponent(new ScreenBoundsTag());

			return boundsEntity;
		}

		public override void DestroyEntity(WorldEntity worldEntity)
		{
			throw new System.NotImplementedException();
		}
	}
}
