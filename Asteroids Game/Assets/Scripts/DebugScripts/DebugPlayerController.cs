using Interfaces.Services;
using Services;
using UnityEngine;

namespace DebugScripts
{
	public class DebugPlayerController : MonoBehaviour
	{
		[SerializeField]
		private float _acceleration = 1;

		[SerializeField]
		private	float _rotationSpeed = 100;

		private IInputService _inputService;

		private Vector3 _currentSpeed;

		private void Awake()
		{
			_inputService = AllServices.Container.GetSingle<IInputService>();
		}

		private void Update()
		{
			if (_inputService.Accelerate)
			{
				_currentSpeed += _acceleration * Time.deltaTime * transform.up;
			}

			if (_inputService.RotateLeft ^ _inputService.RotateRight)
			{
				var rotationSign = _inputService.RotateRight ? -1 : 1;
				transform.Rotate(0, 0, _rotationSpeed * rotationSign * Time.deltaTime);
			}

			transform.position += _currentSpeed * Time.deltaTime;
		}
	}
}
