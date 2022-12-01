using System;
using GameLoop.StateMachine.StateEnterArgs;
using Interfaces.Services;
using Interfaces.UIContexts;
using UI.Contexts;
using UnityEngine;

namespace GameLoop.StateMachine.States
{
	public class PlayerDeathState : IExitableState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IUISystem _uiSystem;
		private ILevel _level;
		private IResultContext _resultContext;

		public PlayerDeathState(GameStateMachine stateMachine, IUISystem uiSystem)
		{
			_stateMachine = stateMachine;
			_uiSystem = uiSystem;
		}

		public void Enter(StateEnterArgs.StateEnterArgs args)
		{
			if (args is not LevelEndedStateArgs levelEndedArgs)
			{
				throw new ArgumentException("Wrong arguments type");
			}

			_level = levelEndedArgs.Level;
			_resultContext = new ResultContext(_level.CoreContext.KillScore);
			_resultContext.OnRestartRequested += RestartRequestedHandler;
			_uiSystem.ShowResults(_resultContext);
		}

		public void Exit()
		{
			_uiSystem.CloseCurrentScreen();
		}

		private void RestartRequestedHandler()
		{
			_resultContext.OnRestartRequested -= RestartRequestedHandler;
			_level.Clear();
			_stateMachine.Enter<StartLevelState>(StateEnterArgs.StateEnterArgs.Empty);
		}
	}
}
