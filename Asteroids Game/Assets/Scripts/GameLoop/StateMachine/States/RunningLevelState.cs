namespace GameLoop.StateMachine.States
{
	public class RunningLevelState : IGameState
	{
		private readonly GameStateMachine _stateMachine;

		public RunningLevelState(GameStateMachine stateMachine)
		{
			_stateMachine = stateMachine;
		}

		public void Enter(StateEnterArgs.StateEnterArgs args)
		{
		}
	}
}
