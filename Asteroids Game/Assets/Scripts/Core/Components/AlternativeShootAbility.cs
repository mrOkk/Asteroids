namespace Core.WorldEntities
{
	public class AlternativeShootAbility : IComponent
	{
		public bool Requested;
		public int Count;
		public int MaxCount;
		public float LastActivationTime;
		public float Cooldown;
		public float LastRestoredTime;
		public float RestoreTime;
	}
}
