namespace Core.WorldEntities
{
	public class ViewLink<TView> : IComponent where TView : EntityView
	{
		public TView Value;
	}
}
