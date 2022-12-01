using Core.WorldEntities;
using CoreGameplay.Components.Tags;

namespace CoreGameplay.Components.Collisions
{
	public class CollisionWithMissilesHandler : DamageCollisionHandler
	{
		protected override bool Filter(WorldEntity self, WorldEntity effector) => effector.HasComponent<MissileTag>();
	}
}
