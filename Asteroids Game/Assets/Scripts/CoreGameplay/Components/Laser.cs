using System;
using CoreSystem;

namespace CoreGameplay.Components
{
	public class Laser : IComponent
	{
		public Action<WorldEntity, WorldEntity> OnHitDetected;

		public bool Handled;
		public float Length;
	}
}
