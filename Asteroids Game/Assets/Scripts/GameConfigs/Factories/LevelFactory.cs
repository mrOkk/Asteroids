using Configs;
using Core;
using Core.Pools;
using Core.Systems;
using Interfaces.Services;
using Services;
using UI.Contexts;
using UnityEngine;

namespace Spawning.Factories
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
			_inputService = services.GetSingle<IInputService>();
			_cameraService = services.GetSingle<ICameraService>();
			_gameConfig = gameConfig;
			_componentsPool ??= new ComponentsPool();

			foreach (var factory in _allFactories)
			{
				factory.Initialize(services, _componentsPool);
			}
		}

		public ILevel PrepareLevel()
		{
			var coreLoopRunner = Instantiate(_coreLoopRunnerPrefab);
			coreLoopRunner.enabled = false;
			var coreContext = new CoreContext();
			var level = new Level(coreLoopRunner, coreContext);

			coreLoopRunner.RegisterSystem(new InputHandlingSystem(_inputService, _gameConfig.Player));
			coreLoopRunner.RegisterSystem(new FireSystem(coreLoopRunner));
			coreLoopRunner.RegisterSystem(new AlternativeFireSystem());
			coreLoopRunner.RegisterSystem(new AccelerationSystem());
			coreLoopRunner.RegisterSystem(new MovementSystem());
			coreLoopRunner.RegisterSystem(new RotatingSystem());
			coreLoopRunner.RegisterSystem(new CollisionDetectionSystem());
			coreLoopRunner.RegisterSystem(new EnemySpawnSystem(coreLoopRunner
				, _enemiesFactory
				, _cameraService
				, _gameConfig.EnemySpawnTime
				, _gameConfig.SpawnOffsetFromEdge));
			coreLoopRunner.RegisterSystem(new DeathSystem(coreLoopRunner));
			coreLoopRunner.RegisterSystem(new LevelEndSystem(level));
			coreLoopRunner.RegisterSystem(new UIUpdateSystem(coreContext));
			coreLoopRunner.RegisterSystem(new LifetimeSystem(coreLoopRunner));
			coreLoopRunner.RegisterSystem(new KillScoreSystem(coreContext));

			coreLoopRunner.AddEntity(_playerFactory.SpawnEntity());
			coreLoopRunner.AddEntity(_screenBoundsFactory.SpawnEntity());

			return level;
		}
	}
}
