using System.Collections.Generic;
using Core.Systems;
using Core.WorldEntities;
using UnityEngine;

namespace Core
{
	public class CoreLoopRunner : MonoBehaviour
	{
		private List<CoreSystem> _systems = new(10);
		private List<WorldEntity> _allEntities = new(40);
		private List<WorldEntity> _entitiesToDelete = new(10);
		private List<WorldEntity> _entitiesToAdd = new(10);

		public void RegisterSystem(CoreSystem system)
		{
			_systems.Add(system);
		}

		public void Clear()
		{
			foreach (var system in _systems)
			{
				system.Clear();
			}

			_systems.Clear();

			foreach (var entity in _allEntities)
			{
				entity.Destroy();
			}

			_allEntities.Clear();
			_entitiesToAdd.Clear();
			_entitiesToDelete.Clear();
		}

		public void AddEntity(WorldEntity entity)
		{
			_entitiesToAdd.Add(entity);
		}

		public void DestroyEntity(WorldEntity entity)
		{
			_entitiesToDelete.Add(entity);
		}

		private void Update()
		{
			UpdateTick();
			RemoveTickEntities();
			AddTickEntities();
		}

		private void UpdateTick()
		{
			var deltaTime = Time.deltaTime;

			foreach (var system in _systems)
			{
				system.Run(deltaTime);
			}
		}

		private void RemoveTickEntities()
		{
			if (_entitiesToDelete.Count <= 0)
			{
				return;
			}

			foreach (var entity in _entitiesToDelete)
			{
				foreach (var system in _systems)
				{
					system.RemoveEntity(entity);
				}

				_allEntities.Remove(entity);
				entity.Destroy();
			}

			_entitiesToDelete.Clear();
		}

		private void AddTickEntities()
		{
			if (_entitiesToAdd.Count <= 0)
			{
				return;
			}

			foreach (var entity in _entitiesToAdd)
			{
				foreach (var system in _systems)
				{
					system.AddEntity(entity);
				}

				_allEntities.Add(entity);
			}

			_entitiesToAdd.Clear();
		}
	}
}
