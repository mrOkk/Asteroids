using System;
using UnityEngine;

namespace Interfaces.UIContexts
{
	public interface ICoreContext
	{
		event Action OnCoordinatesChanged;
		event Action OnAngleChanged;
		event Action OnSpeedChanged;
		event Action OnLaserCountChanged;
		event Action OnLaserRestoreTimeChanged;

		Vector2 Coordinates { get; set; }
		float Angle { get; set; }
		float Speed { get; set; }
		int LaserCount { get; set; }
		float LaserRestoreTime { get; set; }
		int KillScore { get; set; }
	}
}
