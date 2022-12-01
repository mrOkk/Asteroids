using System;
using System.Collections.Generic;
using GameLoop.StateMachine.States;
using Interfaces.Services;
using Services;

namespace GameLoop.StateMachine
{
	public class GameStateMachine
	{
		private readonly Dictionary<Type, IGameState> _states;
		private IGameState _currentState;

		public GameStateMachine()
		{
			_states = new Dictionary<Type, IGameState>();
		}

		public void Init(AllServices services)
		{
			_states.Add(typeof(LoadingState)
				, new LoadingState(this, services.GetSingle<IUISystem>()));
			_states.Add(typeof(StartLevelState)
				, new StartLevelState(this
					, services.GetSingle<ILevelFactory>()
					, services.GetSingle<ICameraService>()));
			_states.Add(typeof(RunningLevelState)
				, new RunningLevelState(this
					, services.GetSingle<IInputService>()
					, services.GetSingle<IUISystem>()));
			_states.Add(typeof(PlayerDeathState)
				, new PlayerDeathState(this
					, services.GetSingle<IUISystem>()));
		}

		public void Enter<TState>(StateEnterArgs.StateEnterArgs args) where TState : class, IGameState
		{
			ExitState(_currentState as IExitableState);
			_currentState = GetState<TState>();
			EnterState(_currentState, args);
		}

		private void ExitState(IExitableState exitableState)
		{
			exitableState?.Exit();
		}

		private void EnterState(IGameState state, StateEnterArgs.StateEnterArgs args)
		{
			state?.Enter(args);
		}

		private TState GetState<TState>() where TState : class, IGameState
		{
			if (!_states.TryGetValue(typeof(TState), out var newState))
			{
				throw new Exception($"State of type {typeof(TState).Name} is not registered");
			}

			return newState as TState;
		}
	}
}
