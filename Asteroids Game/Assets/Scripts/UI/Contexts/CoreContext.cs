using System;
using Core.WorldEntities;
using Interfaces.UIContexts;
using UnityEngine;

namespace UI.Contexts
{
	public class CoreContext : ICoreContext
	{
		private const float ACCURACY = 0.01f;

		public event Action OnCoordinatesChanged;
		public event Action OnAngleChanged;
		public event Action OnSpeedChanged;
		public event Action OnLaserCountChanged;
		public event Action OnLaserRestoreTimeChanged;

		private Vector2 _coordinates;
		public Vector2 Coordinates
		{
			get => _coordinates;
			set
			{
				if (Math.Abs(value.x - _coordinates.x) < ACCURACY
					&& Math.Abs(value.y - _coordinates.y) < ACCURACY)
				{
					return;
				}

				_coordinates = value;
				OnCoordinatesChanged?.Invoke();
			}
		}

		private float _angle;
		public float Angle
		{
			get => _angle;
			set
			{
				if (Math.Abs(value - _angle) < ACCURACY)
				{
					return;
				}

				_angle = value;
				OnAngleChanged?.Invoke();
			}
		}

		private float _speed;
		public float Speed
		{
			get => _speed;
			set
			{
				if (Math.Abs(value - _speed) < ACCURACY)
				{
					return;
				}

				_speed = value;
				OnSpeedChanged?.Invoke();
			}
		}

		private int _laserCount;
		public int LaserCount
		{
			get => _laserCount;
			set
			{
				if (value == _laserCount)
				{
					return;
				}

				_laserCount = value;
				OnLaserCountChanged?.Invoke();
			}
		}

		private float _laserRestoreTime;
		public float LaserRestoreTime
		{
			get => _laserRestoreTime;
			set
			{
				if (Math.Abs(value - _laserRestoreTime) < ACCURACY)
				{
					return;
				}

				_laserRestoreTime = value;
				OnLaserRestoreTimeChanged?.Invoke();
			}
		}

		public int KillScore { get; set; }

		public WorldEntity PlayerEntity { get; set; }
	}
}
