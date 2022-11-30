using GameLoop.StateMachine.StateEnterArgs;
using Interfaces.Services;

namespace GameLoop.StateMachine.States
{
	public class StartLevelState : IGameState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly ILevelFactory _levelFactory;
		private readonly ICameraService _cameraService;

		public StartLevelState(GameStateMachine stateMachine, ILevelFactory levelFactory, ICameraService cameraService)
		{
			_stateMachine = stateMachine;
			_levelFactory = levelFactory;
			_cameraService = cameraService;
		}

		public void Enter(StateEnterArgs.StateEnterArgs args)
		{
			_cameraService.UpdateWorldScreenSize();
			var level = _levelFactory.PrepareLevel();

			_stateMachine.Enter<RunningLevelState>(new RunningLevelStateArgs(level));
		}
	}
}
