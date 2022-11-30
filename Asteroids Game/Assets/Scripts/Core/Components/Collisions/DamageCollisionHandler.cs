namespace Core.WorldEntities
{
	public abstract class DamageCollisionHandler : ICollisionHandler
	{
		protected abstract bool Filter(WorldEntity self, WorldEntity effector);

		public void Handle(WorldEntity self, WorldEntity effector, ECollisionEventType eventType)
		{
			if (!Filter(self, effector)
				|| !self.HasComponent<Health>()
				|| eventType == ECollisionEventType.Ended)
			{
				return;
			}

			var health = self.GetComponent<Health>();
			health.Value -= 1;
		}
	}
}
