namespace Core.WorldEntities
{
	public class EnemyCollisionHandler : DamageCollisionHandler
	{
		protected override bool Filter(WorldEntity self, WorldEntity effector) => effector.HasComponent<MissileTag>();
	}
}