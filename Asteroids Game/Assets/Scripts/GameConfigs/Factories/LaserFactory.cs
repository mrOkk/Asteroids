using Core.WorldEntities;
using UnityEngine;

namespace Spawning.Factories
{
	[CreateAssetMenu(menuName = "Asteroids/Factories/Laser")]
	public class LaserFactory : SceneObjectFactory
	{
		// TODO Use config
		[SerializeField]
		public GameObject _config;

		public override WorldEntity SpawnEntity(Vector2 position = default)
		{
			throw new System.NotImplementedException();
		}

		public override void DestroyEntity(WorldEntity worldEntity)
		{
			throw new System.NotImplementedException();
		}
	}
}
