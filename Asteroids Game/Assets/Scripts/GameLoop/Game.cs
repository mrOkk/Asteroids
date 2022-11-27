using System.Threading.Tasks;
using Extensions;
using GameLoop.StateMachine;
using GameLoop.StateMachine.StateEnterArgs;
using GameLoop.StateMachine.States;
using Input;
using Interfaces.Services;
using ResourceLoading;
using Services;

namespace GameLoop
{
	public class Game
	{
		private readonly GameStateMachine _stateMachine;
		private readonly AllServices _allServices;

		public Game()
		{
			_allServices = AllServices.Container;
			_stateMachine = new GameStateMachine();
		}

		public async void Run()
		{
			await RegisterServices();
			_stateMachine.Init(_allServices);
			_stateMachine.Enter<LoadingState>(new LoadingStateArgs());
		}

		private async Task RegisterServices()
		{
			var resourceLoader = await new ResourceLoader().WaitForServiceReady();
			_allServices.RegisterSingle<IResourceLoader>(resourceLoader);

			var inputService = await new UnityNewInputSystem(resourceLoader).WaitForServiceReady();
			_allServices.RegisterSingle<IInputService>(inputService);
		}
	}
}
