using Core.WorldEntities.DeathBehaviour;
using Spawning.Factories;

namespace Core.WorldEntities
{
	public class CustomDeathBehaviour : IComponent
	{
		public IDeathHandler Handler;
	}
}
