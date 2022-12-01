using Core;
using Core.WorldEntities;

namespace CoreGameplay.Components
{
	public class ViewLink<TView> : IComponent where TView : EntityView
	{
		public TView Value;
	}
}
