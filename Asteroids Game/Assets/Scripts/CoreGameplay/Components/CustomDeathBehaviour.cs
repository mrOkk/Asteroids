using Core.WorldEntities;
using CoreGameplay.Components.DeathBehaviour;

namespace CoreGameplay.Components
{
	public class CustomDeathBehaviour : IComponent
	{
		public IDeathHandler Handler;
	}
}
