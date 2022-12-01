using System;
using System.Collections.Generic;
using Core.WorldEntities;

namespace Core.Pools
{
	public class ComponentsPool
	{
		private Dictionary<Type, Stack<IComponent>> _components = new();

		public TComponent Get<TComponent>() where TComponent : IComponent, new()
		{
			if (_components.TryGetValue(typeof(TComponent), out var components))
			{
				if (components.Count > 0)
				{
					return (TComponent)components.Pop();
				}
			}

			return new TComponent();
		}

		public void Return(IComponent component)
		{
			var type = component.GetType();

			if (!_components.TryGetValue(type, out var components))
			{
				components = new Stack<IComponent>();
				_components[type] = components;
			}

			components.Push(component);
		}
	}
}
