using Core.WorldEntities;

namespace CoreGameplay.Components.Collisions
{
	public interface ICollisionHandler
	{
		void Handle(WorldEntity self, WorldEntity effector, ECollisionEventType eventType);
	}
}