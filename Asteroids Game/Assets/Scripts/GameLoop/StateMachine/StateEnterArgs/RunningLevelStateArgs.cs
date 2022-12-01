using Interfaces.Services;

namespace GameLoop.StateMachine.StateEnterArgs
{
	public class RunningLevelStateArgs : StateEnterArgs
	{
		public ILevel Level { get; }

		public RunningLevelStateArgs(ILevel level)
		{
			Level = level;
		}
	}
}
