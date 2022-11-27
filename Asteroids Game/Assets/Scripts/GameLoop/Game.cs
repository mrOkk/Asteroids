using System.Threading.Tasks;
using GameLoop.StateMachine;
using GameLoop.StateMachine.StateEnterArgs;
using GameLoop.StateMachine.States;
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

		public async Task RegisterServices()
		{

		}
	}
}
