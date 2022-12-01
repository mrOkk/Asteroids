namespace Core.WorldEntities
{
	public interface ICollisionHandler
	{
		void Handle(WorldEntity self, WorldEntity effector, ECollisionEventType eventType);
	}
}