using Interfaces.Services;
using UnityEngine;

namespace Core.WorldEntities
{
	public class ScreenBoundsCheckCollisionHandler : ICollisionHandler
	{
		private readonly ICameraService _cameraService;

		public ScreenBoundsCheckCollisionHandler(ICameraService cameraService)
		{
			_cameraService = cameraService;
		}

		public void Handle(WorldEntity self, WorldEntity effector, ECollisionEventType eventType)
		{
			if (eventType != ECollisionEventType.Ended
				|| !self.HasComponent<TransformLink>()
				|| !effector.HasComponent<ScreenBoundsTag>())
			{
				return;
			}

			var transform = self.GetComponent<TransformLink>().Transform;
			var currentPosition = transform.position;
			var halfScreenSize = 0.5f * _cameraService.WorldScreenSize;
			var cameraPosition = (Vector2)_cameraService.MainCamera.transform.position;
			var bottomLeft = cameraPosition - halfScreenSize;
			var topRight = cameraPosition + halfScreenSize;

			for (var i = 0; i < 2; i++)
			{
				currentPosition[i] = WarpByAxis(currentPosition[i], bottomLeft[i], topRight[i]);
			}

			transform.position = currentPosition;
		}

		private float WarpByAxis(float axisValue, float axisLowBound, float axisHighBound)
		{
			if (axisValue < axisLowBound)
			{
				axisValue = axisHighBound + (axisLowBound - axisValue);
			}
			else if (axisValue > axisHighBound)
			{
				axisValue = axisLowBound + (axisHighBound - axisValue);
			}

			return axisValue;
		}
	}
}
