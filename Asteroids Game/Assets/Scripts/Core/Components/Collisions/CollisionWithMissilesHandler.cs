namespace Core.WorldEntities
{
	public class CollisionWithMissilesHandler : DamageCollisionHandler
	{
		protected override bool Filter(WorldEntity self, WorldEntity effector) => effector.HasComponent<MissileTag>();
	}
}
