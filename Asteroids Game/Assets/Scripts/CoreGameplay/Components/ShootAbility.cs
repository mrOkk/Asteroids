using Core.WorldEntities;
using GameConfigs.Factories;

namespace CoreGameplay.Components
{
	public class ShootAbility : IComponent
	{
		public bool Requested;
		public float Cooldown;
		public float LastActivationTime;

		public SceneObjectFactory ShootFactory;
	}
}
