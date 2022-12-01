using Core;
using CoreSystem;
using CoreSystem.Interfaces;

namespace CoreGameplay.Components.DeathBehaviour
{
	public interface IDeathHandler
	{
		void Handle(WorldEntity owner, ICoreWorld runner);
	}
}
