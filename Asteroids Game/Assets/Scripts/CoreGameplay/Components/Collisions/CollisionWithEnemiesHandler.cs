using CoreGameplay.Components.Tags;
using CoreSystem;

namespace CoreGameplay.Components.Collisions
{
	public class CollisionWithEnemiesHandler : DamageCollisionHandler
	{
		// here could be some invincibility tag for self
		protected override bool Filter(WorldEntity self, WorldEntity effector) => effector.HasComponent<EnemyTag>();
	}
}
