using System;

namespace Interfaces.UIContexts
{
	public interface IResultContext
	{
		event Action OnRestartRequested;

		int Result { get; }

		void Restart();
	}
}
