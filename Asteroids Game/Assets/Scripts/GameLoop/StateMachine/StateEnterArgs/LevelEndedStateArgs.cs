using Interfaces.Services;

namespace GameLoop.StateMachine.StateEnterArgs
{
	public class LevelEndedStateArgs : StateEnterArgs
	{
		public ILevel Level { get; }

		public LevelEndedStateArgs(ILevel level)
		{
			Level = level;
		}
	}
}
