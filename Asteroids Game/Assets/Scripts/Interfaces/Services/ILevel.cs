using System;

namespace Interfaces.Services
{
	public interface ILevel
	{
		event Action OnEnded;

		void Run();
		void Stop();
		void Clear();
		void End();
	}
}
