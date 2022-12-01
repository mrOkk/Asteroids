using Core;
using Core.Pools;
using CoreGameplay;
using CoreGameplay.Systems;
using GameConfigs.Configs;
using Interfaces.Services;
using Services;
using UI.Contexts;
using UnityEngine;

namespace GameConfigs.Factories
{
	[CreateAssetMenu(fileName = "LevelFactory", menuName = "Asteroids/LevelFactory")]
	public class LevelFactory : ScriptableObject, ILevelFactory
	{
		public string LevelSceneName = "Game";

		[SerializeField]
		private SceneObjectFactory _playerFactory;
		[SerializeField]
		private SceneObjectFactory _enemiesFactory;
		[SerializeField]
		private SceneObjectFactory _screenBoundsFactory;

		[SerializeField]
		private SceneObjectFactory[] _allFactories;

		private CoreLoopRunner _coreLoopRunnerPrefab;
		private CoreLoopRunner _coreLoopRunner;

		private IInputService _inputService;
		private ICameraService _cameraService;
		private GameConfig _gameConfig;
		private ComponentsPool _componentsPool;

		public void Initialize(CoreLoopRunner coreLoopRunnerPrefab
			, AllServices services
			, GameConfig gameConfig)
		{
			_coreLoopRunnerPrefab = coreLoopRunnerPrefab;
			_coreLoopRunner = Instantiate(_coreLoopRunnerPrefab);
			_coreLoopRunner.SetActive(false);
			DontDestroyOnLoad(_coreLoopRunner);

			_inputService = services.GetSingle<IInputService>();
			_cameraService = services.GetSingle<ICameraService>();
			_gameConfig = gameConfig;
			_componentsPool ??= new ComponentsPool();

			foreach (var factory in _allFactories)
			{
				factory.Initialize(services, _componentsPool, _coreLoopRunner);
			}
		}

		public ILevel PrepareLevel()
		{
			var coreContext = new CoreContext();
			var level = new Level(_coreLoopRunner, coreContext);

			_coreLoopRunner.RegisterSystem(new InputHandlingSystem(_inputService, _gameConfig.Player));
			_coreLoopRunner.RegisterSystem(new FireSystem(_coreLoopRunner));
			_coreLoopRunner.RegisterSystem(new AlternativeFireSystem(_coreLoopRunner));
			_coreLoopRunner.RegisterSystem(new AccelerationSystem());
			_coreLoopRunner.RegisterSystem(new MovementSystem());
			_coreLoopRunner.RegisterSystem(new RotatingSystem());
			_coreLoopRunner.RegisterSystem(new CollisionDetectionSystem());
			_coreLoopRunner.RegisterSystem(new EnemySpawnSystem(_coreLoopRunner
				, _enemiesFactory
				, _cameraService
				, _gameConfig.EnemySpawnTime
				, _gameConfig.SpawnOffsetFromEdge));
			_coreLoopRunner.RegisterSystem(new DeathSystem(_coreLoopRunner));
			_coreLoopRunner.RegisterSystem(new LevelEndSystem(level));
			_coreLoopRunner.RegisterSystem(new UIUpdateSystem(coreContext));
			_coreLoopRunner.RegisterSystem(new LifetimeSystem(_coreLoopRunner));
			_coreLoopRunner.RegisterSystem(new KillScoreSystem(coreContext));
			_coreLoopRunner.RegisterSystem(new LaserShootSystem());
			_coreLoopRunner.RegisterSystem(new FollowSystem());

			_coreLoopRunner.AddEntity(_playerFactory.SpawnEntity());
			_coreLoopRunner.AddEntity(_screenBoundsFactory.SpawnEntity());

			return level;
		}
	}
}
