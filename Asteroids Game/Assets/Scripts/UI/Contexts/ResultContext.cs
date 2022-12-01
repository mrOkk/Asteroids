using System;
using Interfaces.UIContexts;

namespace UI.Contexts
{
	public class ResultContext : IResultContext
	{
		public event Action OnRestartRequested;

		public int Result { get; }

		public ResultContext(int result)
		{
			Result = result;
		}

		public void Restart()
		{
			OnRestartRequested?.Invoke();
		}
	}
}
