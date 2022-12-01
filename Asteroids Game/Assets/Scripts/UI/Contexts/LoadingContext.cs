using Interfaces.UIContexts;

namespace UI.Contexts
{
	public class LoadingContext : ILoadingContext
	{
		public int Progress { get; }
	}
}
