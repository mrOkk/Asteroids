using System.Collections.Generic;

namespace CoreSystem.Interfaces
{
	public interface ICoreWorld
	{
		IReadOnlyList<WorldEntity> AllEntities { get; }

		void SetActive(bool active);
		void RegisterSystem(CoreSystem system);
		void Clear();
		void AddEntity(WorldEntity entity);
		void DestroyEntity(WorldEntity entity);
	}
}
