using System;
using Interfaces.Services;
using Interfaces.UIContexts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
	public class Level : ILevel
	{
		public event Action OnEnded;

		private CoreLoopRunner _runner;

		public ICoreContext CoreContext { get; }

		public Level(CoreLoopRunner runner, ICoreContext coreContext)
		{
			_runner = runner;
			CoreContext = coreContext;
		}

		public void Run()
		{
			_runner.enabled = true;
		}

		public void Stop()
		{
			_runner.enabled = false;
		}

		public void Clear()
		{
			_runner.Clear();
			Object.Destroy(_runner);
		}

		public void End()
		{
			OnEnded?.Invoke();
		}
	}
}
