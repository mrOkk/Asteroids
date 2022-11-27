using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
	[RequireComponent(typeof(PlayerInput))]
	public class InputHandlingBehaviour : MonoBehaviour
	{
		public event Action<bool> OnAccelerateChanged;
		public event Action<bool> OnRotateRightChanged;
		public event Action<bool> OnRotateLeftChanged;
		public event Action<bool> OnFireChanged;
		public event Action<bool> OnAlternativeFireChanged;

		[SerializeField]
		private PlayerInput _playerInput;

		[SerializeField]
		private InputActionReference _accelerateAction;
		[SerializeField]
		private InputActionReference _rotateRightAction;
		[SerializeField]
		private InputActionReference _rotateLeftAction;
		[SerializeField]
		private InputActionReference _fireAction;
		[SerializeField]
		private InputActionReference _alternativeFireAction;

		private void Awake()
		{
			if (_playerInput == null)
			{
				_playerInput = GetComponent<PlayerInput>();
			}

			_playerInput.onActionTriggered += InputActionTriggeredHandler;
		}

		private void InputActionTriggeredHandler(InputAction.CallbackContext context)
		{
			if (context.action == _accelerateAction.action)
			{
				HandleInputStatus(context, OnAccelerateChanged);
			}

			if (context.action == _rotateRightAction.action)
			{
				HandleInputStatus(context, OnRotateRightChanged);
			}

			if (context.action == _rotateLeftAction.action)
			{
				HandleInputStatus(context, OnRotateLeftChanged);
			}

			if (context.action == _fireAction.action)
			{
				HandleInputStatus(context, OnFireChanged);
			}

			if (context.action == _alternativeFireAction.action)
			{
				HandleInputStatus(context, OnAlternativeFireChanged);
			}
		}

		private void HandleInputStatus(InputAction.CallbackContext context, Action<bool> inputDelegate)
		{
			if (context.phase == InputActionPhase.Performed)
			{
				CallInputChanged(inputDelegate, true);
			}

			if (context.phase == InputActionPhase.Canceled)
			{
				CallInputChanged(inputDelegate, false);
			}
		}

		private void CallInputChanged(Action<bool> inputDelegate, bool value)
		{
			inputDelegate?.Invoke(value);
		}
	}
}
