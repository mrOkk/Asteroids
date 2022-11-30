using System;
using Interfaces.Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
	public class Level : ILevel
	{
		public event Action OnEnded;

		private CoreLoopRunner _runner;

		public Level(CoreLoopRunner runner)
		{
			_runner = runner;
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
