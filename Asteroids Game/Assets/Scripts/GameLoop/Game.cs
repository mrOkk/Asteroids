using Configs;
using Core;
using GameLoop.StateMachine;
using GameLoop.StateMachine.StateEnterArgs;
using GameLoop.StateMachine.States;
using Input;
using Interfaces.Services;
using ResourceLoading;
using Services;
using UnityEngine;

namespace GameLoop
{
	public class Game
	{
		private readonly GameConfig _gameConfig;
		private readonly GameStateMachine _stateMachine;
		private readonly AllServices _allServices;

		public Game(GameConfig gameConfig)
		{
			_gameConfig = gameConfig;
			_allServices = AllServices.Container;
			_stateMachine = new GameStateMachine();
		}

		public void Run()
		{
			RegisterServices();
			_stateMachine.Init(_allServices);
			_stateMachine.Enter<LoadingState>(new LoadingStateArgs(_gameConfig.LevelFactory.LevelSceneName));
		}

		private void RegisterServices()
		{
			var resourceLoader = new ResourceLoader();
			_allServices.RegisterSingle<IResourceLoader>(resourceLoader);

			var inputService = new UnityNewInputSystem(_gameConfig.InputHandler);
			_allServices.RegisterSingle<IInputService>(inputService);

			var cameraService = new CameraService(Camera.main);
			_allServices.RegisterSingle<ICameraService>(cameraService);

			var levelFactory = _gameConfig.LevelFactory;
			levelFactory.Initialize(_gameConfig.CoreLoopRunner, _allServices, _gameConfig);
			_allServices.RegisterSingle<ILevelFactory>(levelFactory);
		}
	}
}
