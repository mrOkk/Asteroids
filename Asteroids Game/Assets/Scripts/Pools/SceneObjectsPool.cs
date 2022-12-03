using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Pools
{
	public class SceneObjectsPool
	{
		private const string SCENE_OBJECTS_POOL_NAME = "SceneObjectsPool";

		private readonly Dictionary<string, EntityView> _prefabs = new ();
		private readonly Dictionary<string, Stack<EntityView>> _instances = new();

		private Transform _root;

		public SceneObjectsPool()
		{
			var rootGO = new GameObject(SCENE_OBJECTS_POOL_NAME);
			_root = rootGO.transform;
		}

		public void RegisterPrefab(string key, EntityView prefab)
		{
			_prefabs[key] = prefab;
			_instances[key] = new Stack<EntityView>();
		}

		public void Clear()
		{
			foreach (var (_, instancesCollection) in _instances)
			{
				while (instancesCollection.Count > 0)
				{
					var instance = instancesCollection.Pop();
					Object.Destroy(instance);
				}
			}

			_instances.Clear();
			_prefabs.Clear();
		}

		public TView Get<TView>(string key, Vector2 position, Quaternion rotation) where TView : EntityView
		{
			if (!_instances.TryGetValue(key, out var instances))
			{
				throw new KeyNotFoundException("Pool prefab should be registered first");
			}

			var instance = instances.Count > 0 ? instances.Pop() : Object.Instantiate(_prefabs[key]);
			instance.transform.SetParent(null);
			instance.transform.SetPositionAndRotation(position, rotation == default ? Quaternion.identity : rotation);
			instance.gameObject.SetActive(true);

			if (instance is not TView viewInstance)
			{
				throw new Exception($"Wrong type for EntityView. {typeof(TView).Name} expected");
			}

			return viewInstance;
		}

		public void Return(string key, EntityView instance)
		{
			if (!_instances.TryGetValue(key, out var instances))
			{
				throw new KeyNotFoundException("Pool prefab should be registered first");
			}

			instance.gameObject.SetActive(false);
			instance.transform.SetParent(_root);
			instances.Push(instance);
		}
	}
}
