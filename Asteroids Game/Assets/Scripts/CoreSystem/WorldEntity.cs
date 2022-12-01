using System;
using System.Collections.Generic;
using CoreSystem.Interfaces;

namespace CoreSystem
{
	public class WorldEntity
	{
		public IReadOnlyDictionary<Type, IComponent> Components => _components;

		private readonly IEntityDestroyer _destroyer;
		private readonly Dictionary<Type, IComponent> _components = new();

		public WorldEntity(IEntityDestroyer destroyer)
		{
			_destroyer = destroyer;
		}

		public void AddComponent<TComponent>(TComponent component) where TComponent : IComponent
			=> _components[typeof(TComponent)] = component;

		public bool HasComponent<TComponent>() where TComponent : IComponent
			=> _components.ContainsKey(typeof(TComponent));

		public TComponent GetComponent<TComponent>() where TComponent : IComponent
			=> (TComponent)_components[typeof(TComponent)];

		public void Destroy()
		{
			_destroyer.DestroyEntity(this);
			_components.Clear();
		}
	}
}
