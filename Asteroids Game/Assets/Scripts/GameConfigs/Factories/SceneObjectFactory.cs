using Core;
using Core.Pools;
using CoreSystem;
using CoreSystem.Interfaces;
using Services;
using UnityEngine;

namespace GameConfigs.Factories
{
	public abstract class SceneObjectFactory : ScriptableObject, IEntityDestroyer
	{
		protected AllServices Services;
		protected ComponentsPool ComponentsPool;
		protected SceneObjectsPool SceneObjectsPool;
		protected ICoreWorld CoreWorld;

		public virtual void Initialize(
			AllServices services
			, ComponentsPool componentsPool
			, SceneObjectsPool sceneObjectsPool
			, ICoreWorld coreWorld)
		{
			Services = services;
			CoreWorld = coreWorld;
			ComponentsPool = componentsPool;
			SceneObjectsPool = sceneObjectsPool;
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
