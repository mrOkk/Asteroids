namespace Core.WorldEntities
{
	public class PlayerCollisionHandler : DamageCollisionHandler
	{
		// here could be some invincibility tag for self
		protected override bool Filter(WorldEntity self, WorldEntity effector) => effector.HasComponent<EnemyTag>();
	}
}