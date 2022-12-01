using Core.WorldEntities;

namespace Core
{
	public interface IEntityDestroyer
	{
		void DestroyEntity(WorldEntity worldEntity);
	}
}
