using System.Collections.Generic;
using Core.WorldEntities;

namespace CoreGameplay.Components.Collisions
{
	public class CompositeCollisionHandler : ICollisionHandler
	{
		private readonly List<ICollisionHandler> _innerHandlers = new ();

		public void Handle(WorldEntity self, WorldEntity effector, ECollisionEventType eventType)
		{
			foreach (var collisionHandler in _innerHandlers)
			{
				collisionHandler.Handle(self, effector, eventType);
			}
		}

		public void AddCollisionHandler(ICollisionHandler collisionHandler)
		{
			_innerHandlers.Add(collisionHandler);
		}
	}
}