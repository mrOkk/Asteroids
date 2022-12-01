using System;

namespace Core.WorldEntities
{
	public class Laser : IComponent
	{
		public Action<WorldEntity, WorldEntity> OnHitDetected;

		public bool Handled;
		public float Length;
	}
}
