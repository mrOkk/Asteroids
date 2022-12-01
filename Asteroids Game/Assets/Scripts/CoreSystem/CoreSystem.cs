using System.Collections.Generic;
using Core.WorldEntities;

namespace Core.Systems
{
	public abstract class CoreSystem
	{
		protected List<WorldEntity> Entities = new();

		public virtual void AddEntity(WorldEntity entity)
		{
			if (Filter(entity))
			{
				Entities.Add(entity);
			}
		}

		public virtual void RemoveEntity(WorldEntity entity)
		{
			if (Filter(entity))
			{
				Entities.Remove(entity);
			}
		}

		public void Clear() => Entities.Clear();

		public abstract void Run(float deltaTime);
		protected abstract bool Filter(WorldEntity entity);
	}
}
