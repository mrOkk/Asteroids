using Core;
using CoreGameplay.Components;
using CoreGameplay.Components.Tags;
using CoreSystem;
using CoreSystem.Interfaces;
using GameConfigs.Factories;
using Interfaces.Services;
using UnityEngine;

namespace CoreGameplay.Systems
{
	public class EnemySpawnSystem : CoreSystem.CoreSystem
	{
		private readonly ICoreWorld _world;
		private readonly SceneObjectFactory _enemiesFactory;
		private readonly ICameraService _cameraService;
		private readonly SceneObjectFactory _factory;

		private float _lastSpawnTime;
		private float _spawnCooldown = 2f;
		private float _spawnOffsetFromEdge;
		private int _maxEnemiesCount = 10;

		public EnemySpawnSystem(ICoreWorld world
			, SceneObjectFactory enemiesFactory
			, ICameraService cameraService
			, float spawnCooldown
			, float spawnOffsetFromEdge)
		{
			_world = world;
			_enemiesFactory = enemiesFactory;
			_cameraService = cameraService;
			_spawnCooldown = spawnCooldown;
			_spawnOffsetFromEdge = spawnOffsetFromEdge;
		}

		public override void Run(float deltaTime)
		{
			if (Entities.Count >= _maxEnemiesCount)
			{
				return;
			}

			if (Time.time - _lastSpawnTime < _spawnCooldown)
			{
				return;
			}

			var position = GeneratePosition();
			var newEntity = _enemiesFactory.SpawnEntity(position);

			if (newEntity.HasComponent<Movement>())
			{
				newEntity.GetComponent<Movement>().Direction = GenerateDirection(position);
			}

			_world.AddEntity(newEntity);
			_lastSpawnTime = Time.time;
		}

		protected override bool Filter(WorldEntity entity) => entity.HasComponent<EnemyTag>();

		private Vector2 GeneratePosition()
		{
			var position = Vector2.zero;
			var axis = Random.Range(0, 2);
			var cameraPosition = _cameraService.MainCamera.transform.position;
			var screenHalfSize = 0.5f * _cameraService.WorldScreenSize;
			position[axis] = Random.Range(cameraPosition[axis] - screenHalfSize[axis],
				cameraPosition[axis] + screenHalfSize[axis]);
			axis = (axis + 1) % 2;
			var sign = Random.Range(0, 2) == 1 ? 1f : -1f;
			position[axis] = cameraPosition[axis] + sign * (screenHalfSize[axis] + _spawnOffsetFromEdge);

			return position;
		}

		private Vector2 GenerateDirection(Vector2 startPosition)
		{
			var cameraPosition = _cameraService.MainCamera.transform.position;
			var screenHalfSize = 0.5f * _cameraService.WorldScreenSize;
			var targetPoint = new Vector2(Random.Range(cameraPosition.x - screenHalfSize.x, cameraPosition.x + screenHalfSize.x)
				, Random.Range(cameraPosition.y - screenHalfSize.y, cameraPosition.y + screenHalfSize.y));
			var direction = (targetPoint - startPosition).normalized;

			return direction;
		}
	}
}
