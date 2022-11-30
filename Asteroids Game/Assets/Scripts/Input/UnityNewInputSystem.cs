using System.Threading.Tasks;
using Interfaces.Services;
using UnityEngine;

namespace Input
{
	public class UnityNewInputSystem : IInputService
	{
		private bool _accelerate;
		public bool Accelerate => _accelerate;

		private bool _rotateRight;
		public bool RotateRight => _rotateRight;

		private bool _rotateLeft;
		public bool RotateLeft => _rotateLeft;

		private bool _fire;
		public bool Fire => _fire;

		private bool _alternativeFire;
		public bool AlternativeFire => _alternativeFire;

		private readonly IResourceLoader _resourceLoader;
		private readonly TaskCompletionSource<object> _initializationTask;

		private InputHandlingBehaviour _inputHandler;

		private bool _isActive;

		public UnityNewInputSystem(InputHandlingBehaviour inputHandlerPrefab)
		{
			_initializationTask = new TaskCompletionSource<object>();
			Initialize(inputHandlerPrefab);
		}

		public void SetActive(bool active)
		{
			_isActive = active;

			_accelerate = false;
			_rotateLeft = false;
			_rotateRight = false;
			_fire = false;
			_alternativeFire = false;
		}

		private void Initialize(InputHandlingBehaviour inputHandlerPrefab)
		{
			_inputHandler = Object.Instantiate(inputHandlerPrefab);

			_inputHandler.OnAccelerateChanged += AccelerateChangedHandler;
			_inputHandler.OnRotateRightChanged += RotateRightChangedHandler;
			_inputHandler.OnRotateLeftChanged += RotateLeftChangedHandler;
			_inputHandler.OnFireChanged += FireChangedHandler;
			_inputHandler.OnAlternativeFireChanged += AlternativeFireChangedHandler;

			Object.DontDestroyOnLoad(_inputHandler);

			_initializationTask.SetResult(null);
		}

		private void AccelerateChangedHandler(bool value)
		{
			SetProperty(ref _accelerate, value);
		}

		private void RotateRightChangedHandler(bool value)
		{
			SetProperty(ref _rotateRight, value);
		}

		private void RotateLeftChangedHandler(bool value)
		{
			SetProperty(ref _rotateLeft, value);
		}

		private void FireChangedHandler(bool value)
		{
			SetProperty(ref _fire, value);
		}

		private void AlternativeFireChangedHandler(bool value)
		{
			SetProperty(ref _alternativeFire, value);
		}

		private void SetProperty(ref bool property, bool value)
		{
			if (_isActive)
			{
				property = value;
			}
		}
	}
}
