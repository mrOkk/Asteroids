namespace Core.WorldEntities.DeathBehaviour
{
	public interface IDeathHandler
	{
		void Handle(WorldEntity owner, CoreLoopRunner runner);
	}
}
