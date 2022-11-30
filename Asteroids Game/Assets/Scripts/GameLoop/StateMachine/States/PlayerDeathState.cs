using Interfaces.Services;
using UnityEngine;

namespace GameLoop.StateMachine.States
{
	public class PlayerDeathState : IExitableState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IUISystem _uiSystem;

		public PlayerDeathState(GameStateMachine stateMachine, IUISystem uiSystem)
		{
			_stateMachine = stateMachine;
			_uiSystem = uiSystem;
		}

		public void Enter(StateEnterArgs.StateEnterArgs args)
		{
			// TODO: ui Show death screen
		}

		public void Exit()
		{
			// TODO: ui Show death screen
		}

		private void Restart()
		{
			_stateMachine.Enter<StartLevelState>(StateEnterArgs.StateEnterArgs.Empty);
		}
	}
}
