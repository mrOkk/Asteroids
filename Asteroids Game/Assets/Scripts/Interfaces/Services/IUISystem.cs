using Interfaces.UIContexts;

namespace Interfaces.Services
{
	public interface IUISystem : IService
	{
		void ShowLoadingScreen(ILoadingContext loadingContext);
		void ShowHud(ICoreContext coreContext);
		void ShowResults(IResultContext resultContext);
		void CloseCurrentScreen();
	}
}
