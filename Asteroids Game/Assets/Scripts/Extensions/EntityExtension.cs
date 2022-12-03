using Core;
using Core.Pools;
using CoreGameplay.Components;
using CoreSystem;

namespace Extensions
{
	public static class EntityExtension
	{
		public static void ReturnViewToPool<TView>(this WorldEntity entity, string key, SceneObjectsPool pool)
			where TView : EntityView
		{
			if (entity.HasComponent<ViewLink<TView>>())
			{
				var viewLink = entity.GetComponent<ViewLink<TView>>();
				pool.Return(key, viewLink.Value);
				viewLink.Value = null;
			}
		}
	}
}
