using Core;
using Core.WorldEntities;

namespace CoreGameplay.Components.DeathBehaviour
{
	public interface IDeathHandler
	{
		void Handle(WorldEntity owner, ICoreWorld runner);
	}
}
