using Interfaces.Services;

namespace GameLoop.StateMachine.States
{
	public class RunningLevelState : IExitableState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IInputService _inputService;

		public RunningLevelState(GameStateMachine stateMachine, IInputService inputService)
		{
			_stateMachine = stateMachine;
			_inputService = inputService;
		}

		public void Enter(StateEnterArgs.StateEnterArgs args)
		{
			_inputService.SetActive(true);
		}

		public void Exit()
		{
			_inputService.SetActive(false);
		}
	}
}
