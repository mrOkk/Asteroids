using Core;
using CoreSystem;

namespace CoreGameplay.Components
{
	public class ViewLink<TView> : IComponent where TView : EntityView
	{
		public TView Value;
	}
}
