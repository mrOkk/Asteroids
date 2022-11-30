namespace Core.WorldEntities
{
	public class ShootAbility : IComponent
	{
		public bool Requested;
		public float Cooldown;
		public float LastActivationTime;
	}
}
