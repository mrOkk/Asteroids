namespace GameLoop.StateMachine.States
{
	public class StartLevelState : IGameState
	{
		private readonly GameStateMachine _stateMachine;

		public StartLevelState(GameStateMachine stateMachine)
		{
			_stateMachine = stateMachine;
		}

		public void Enter(StateEnterArgs.StateEnterArgs args)
		{
			_stateMachine.Enter<RunningLevelState>(StateEnterArgs.StateEnterArgs.Empty);
		}
	}
}
