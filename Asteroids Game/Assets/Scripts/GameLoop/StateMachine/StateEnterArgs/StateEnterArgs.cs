namespace GameLoop.StateMachine.StateEnterArgs
{
	public class StateEnterArgs
	{
		private static StateEnterArgs _empty;
		public static StateEnterArgs Empty => _empty ??= new StateEnterArgs();
	}
}
