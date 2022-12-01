using CoreGameplay.Components.Tags;
using CoreSystem;

namespace CoreGameplay.Components.Collisions
{
	public class CollisionWithMissilesHandler : DamageCollisionHandler
	{
		protected override bool Filter(WorldEntity self, WorldEntity effector) => effector.HasComponent<MissileTag>();
	}
}
