using System;
using GameLoop.StateMachine.StateEnterArgs;
using Interfaces.Services;

namespace GameLoop.StateMachine.States
{
	public class RunningLevelState : IExitableState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IInputService _inputService;
		private readonly IUISystem _uiSystem;

		private ILevel _level;

		public RunningLevelState(GameStateMachine stateMachine, IInputService inputService, IUISystem uiSystem)
		{
			_stateMachine = stateMachine;
			_inputService = inputService;
			_uiSystem = uiSystem;
		}

		public void Enter(StateEnterArgs.StateEnterArgs args)
		{
			if (args is not RunningLevelStateArgs runningLevelStateArgs)
			{
				throw new ArgumentException("Wrong arguments type");
			}

			_level = runningLevelStateArgs.Level;
			_level.OnEnded += LevelEndedHandler;
			_level.Run();
			_inputService.SetActive(true);
			_uiSystem.ShowHud(_level.CoreContext);
		}

		public void Exit()
		{
			_level.OnEnded -= LevelEndedHandler;
			_level.Stop();
			_inputService.SetActive(false);
		}

		private void LevelEndedHandler()
		{
			_stateMachine.Enter<PlayerDeathState>(new LevelEndedStateArgs(_level));
		}
	}
}
