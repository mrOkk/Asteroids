namespace GameLoop.StateMachine
{
	public interface IGameState
	{
		void Enter(StateEnterArgs.StateEnterArgs args);
	}

	public interface IExitableState : IGameState
	{
		void Exit();
	}
}
