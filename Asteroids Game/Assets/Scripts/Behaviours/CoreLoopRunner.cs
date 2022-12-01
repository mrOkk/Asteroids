using System.Collections.Generic;
using CoreSystem;
using CoreSystem.Interfaces;
using UnityEngine;

namespace Core
{
	public class CoreLoopRunner : MonoBehaviour, ICoreWorld
	{
		public IReadOnlyList<WorldEntity> AllEntities => _allEntities;

		private List<CoreSystem.CoreSystem> _systems = new(10);
		private List<WorldEntity> _allEntities = new(40);
		private HashSet<WorldEntity> _entitiesToDelete = new(10);
		private HashSet<WorldEntity> _entitiesToAdd = new(10);

		public void SetActive(bool active)
		{
			enabled = active;
		}

		public void RegisterSystem(CoreSystem.CoreSystem system)
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
			AddTickEntities();
			UpdateTick();
			RemoveTickEntities();
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
