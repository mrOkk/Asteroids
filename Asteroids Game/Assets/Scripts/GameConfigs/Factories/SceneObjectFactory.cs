using Core;
using Core.Pools;
using Core.WorldEntities;
using Services;
using UnityEngine;

namespace Spawning.Factories
{
	public abstract class SceneObjectFactory : ScriptableObject, IEntityDestroyer
	{
		protected ComponentsPool ComponentsPool;

		public virtual void Initialize(AllServices services, ComponentsPool componentsPool)
		{
			ComponentsPool = componentsPool;
		}

		public abstract WorldEntity SpawnEntity(Vector2 position = default);

		public virtual void DestroyEntity(WorldEntity worldEntity)
		{
			foreach (var (_, component) in worldEntity.Components)
			{
				ComponentsPool.Return(component);
			}
		}
	}
}
