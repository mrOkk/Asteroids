using System;
using System.Threading.Tasks;
using Extensions;
using GameLoop.StateMachine.StateEnterArgs;
using Interfaces.Services;
using UnityEngine.SceneManagement;

namespace GameLoop.StateMachine.States
{
	public class LoadingState : IGameState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IUISystem _uiSystem;

		public LoadingState(GameStateMachine stateMachine, IUISystem uiSystem)
		{
			_stateMachine = stateMachine;
			_uiSystem = uiSystem;
		}

		public void Enter(StateEnterArgs.StateEnterArgs args)
		{
			if (args is not LoadingStateArgs loadingStateArgs)
			{
				throw new ArgumentException($"Wrong args type for {nameof(LoadingState)}");
			}

			EnterAsync(loadingStateArgs.SceneName).Forget();
		}

		private async Task EnterAsync(string sceneName)
		{
			var operation = SceneManager.LoadSceneAsync(sceneName);

			while (!operation.isDone)
			{
				await Task.Yield();
			}

			_stateMachine.Enter<StartLevelState>(StateEnterArgs.StateEnterArgs.Empty);
		}
	}
}
