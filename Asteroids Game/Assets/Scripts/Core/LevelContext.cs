using Core.WorldEntities;

namespace Core
{
	public class LevelContext
	{
		public WorldEntity Player { get; }

		public LevelContext(WorldEntity player)
		{
			Player = player;
		}
	}
}
