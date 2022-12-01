using System;
using Interfaces.UIContexts;

namespace Interfaces.Services
{
	public interface ILevel
	{
		event Action OnEnded;

		ICoreContext CoreContext { get; }

		void Run();
		void Stop();
		void Clear();
		void End();
	}
}
