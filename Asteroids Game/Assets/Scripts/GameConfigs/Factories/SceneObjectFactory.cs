using Core;
using Core.Pools;
using Core.WorldEntities;
using Services;
using UnityEngine;

namespace GameConfigs.Factories
{
	public abstract class SceneObjectFactory : ScriptableObject, IEntityDestroyer
	{
		protected AllServices Services;
		protected ComponentsPool ComponentsPool;
		protected ICoreWorld CoreWorld;

		public virtual void Initialize(AllServices services
			, ComponentsPool componentsPool
			, ICoreWorld coreWorld)
		{
			Services = services;
			CoreWorld = coreWorld;
			ComponentsPool = componentsPool;
		}

		public abstract WorldEntity SpawnEntity(Vector2 position = default, Quaternion rotation = default);

		public virtual void DestroyEntity(WorldEntity worldEntity)
		{
			foreach (var (_, component) in worldEntity.Components)
			{
				ComponentsPool.Return(component);
			}
		}
	}
}
