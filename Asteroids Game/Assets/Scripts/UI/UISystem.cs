using System;
using Interfaces.Services;
using Interfaces.UIContexts;
using UI.Screens;
using UnityEngine;

namespace UI
{
	public class UISystem : MonoBehaviour, IUISystem
	{
		[SerializeField]
		private LoadingScreen _loadingScreen;
		[SerializeField]
		private HudScreen _hudScreen;
		[SerializeField]
		private ResultScreen _resultScreen;

		[SerializeField]
		private GameObject _uiEventSystem;

		private ScreenBase _currentScreen;

		private void Awake()
		{
			DontDestroyOnLoad(this);
			DontDestroyOnLoad(_uiEventSystem);
		}

		public void ShowLoadingScreen(ILoadingContext loadingContext)
		{
			CloseCurrentScreen();
			_loadingScreen.Show(loadingContext);
			_currentScreen = _loadingScreen;
		}

		public void ShowHud(ICoreContext coreContext)
		{
			CloseCurrentScreen();
			_hudScreen.Show(coreContext);
			_currentScreen = _hudScreen;
		}

		public void ShowResults(IResultContext resultContext)
		{
			CloseCurrentScreen();
			_resultScreen.Show(resultContext);
			_currentScreen = _resultScreen;
		}

		public void CloseCurrentScreen()
		{
			if (_currentScreen == null)
			{
				return;
			}

			_currentScreen.Close();
			_currentScreen = null;
		}
	}
}
