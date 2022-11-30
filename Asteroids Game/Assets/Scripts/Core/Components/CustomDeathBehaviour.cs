using Spawning.Factories;

namespace Core.WorldEntities
{
	public class CustomDeathBehaviour : IComponent
	{
		public int SpawnCount;
		public SceneObjectFactory Factory;
	}
}
