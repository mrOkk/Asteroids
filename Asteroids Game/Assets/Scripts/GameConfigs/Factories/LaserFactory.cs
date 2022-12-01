using Core;
using Core.Pools;
using Core.WorldEntities;
using Services;
using UnityEngine;

namespace Spawning.Factories
{
	[CreateAssetMenu(menuName = "Asteroids/Factories/Laser")]
	public class LaserFactory : SceneObjectFactory
	{
		// TODO Use config
		[SerializeField]
		public EntityView _prefab;

		private SceneObjectsPool<EntityView> _sceneObjectsPool;


		public override void Initialize(AllServices services, ComponentsPool componentsPool)
		{
			base.Initialize(services, componentsPool);
			_sceneObjectsPool = new SceneObjectsPool<EntityView>(_prefab);
		}

		public override WorldEntity SpawnEntity(Vector2 position = default, Quaternion rotation = default)
		{
			throw new System.NotImplementedException();
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
