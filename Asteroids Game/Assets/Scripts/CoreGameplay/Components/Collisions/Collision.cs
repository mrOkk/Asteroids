using CoreSystem;

namespace CoreGameplay.Components.Collisions
{
	public class Collision : IComponent
	{
		public WorldEntity Entity1;
		public WorldEntity Entity2;
		public ECollisionEventType ECollisionEventType;
	}
}
