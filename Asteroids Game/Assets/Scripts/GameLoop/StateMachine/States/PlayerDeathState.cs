using Interfaces.Services;

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

		}

		public void Exit()
		{

		}
	}
}
