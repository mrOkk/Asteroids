using System.Collections.Generic;
using UnityEngine;

namespace Core.Pools
{
	public class SceneObjectsPool<TView> where TView : EntityView
	{
		private readonly TView _prefab;
		private readonly Stack<TView> _instances = new();

		public SceneObjectsPool(TView prefab)
		{
			_prefab = prefab;
		}

		public TView Get(Vector2 position, Quaternion rotation)
		{
			var instance = _instances.Count > 0 ? _instances.Pop() : Object.Instantiate(_prefab);
			instance.transform.SetPositionAndRotation(position, rotation == default ? Quaternion.identity : rotation);
			instance.gameObject.SetActive(true);
			return instance;
		}

		public void Return(TView instance)
		{
			instance.gameObject.SetActive(false);
			_instances.Push(instance);
		}
	}
}
