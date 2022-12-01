using System;
using Core;
using CoreSystem.Interfaces;
using Interfaces.Services;
using Interfaces.UIContexts;

namespace CoreGameplay
{
	public class Level : ILevel
	{
		public event Action OnEnded;

		private ICoreWorld _world;

		public ICoreContext CoreContext { get; }

		public Level(ICoreWorld world, ICoreContext coreContext)
		{
			_world = world;
			CoreContext = coreContext;
		}

		public void Run()
		{
			_world.SetActive(true);
		}

		public void Stop()
		{
			_world.SetActive(false);
		}

		public void Clear()
		{
			_world.Clear();
		}

		public void End()
		{
			OnEnded?.Invoke();
		}
	}
}
