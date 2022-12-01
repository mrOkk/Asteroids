using Interfaces.UIContexts;
using TMPro;
using UnityEngine;

namespace UI.Screens
{
	public class HudScreen : ScreenBase
	{
		private const string FLOAT_FORMAT = "{0:0.0}";
		private const string INT_FORMAT = "{0:0}";

		[SerializeField]
		private TMP_Text _coordinates;
		[SerializeField]
		private TMP_Text _angle;
		[SerializeField]
		private TMP_Text _speed;
		[SerializeField]
		private TMP_Text _laserCount;
		[SerializeField]
		private TMP_Text _laserRestoreTime;

		private ICoreContext _coreContext;

		public void Show(ICoreContext coreContext)
		{
			_coreContext = coreContext;

			SetText(_coordinates, _coreContext.Coordinates, null);
			SetText(_angle, _coreContext.Angle, FLOAT_FORMAT);
			SetText(_speed, _coreContext.Speed, FLOAT_FORMAT);
			SetText(_laserCount, _coreContext.LaserCount, INT_FORMAT);
			SetText(_laserRestoreTime, _coreContext.LaserRestoreTime, FLOAT_FORMAT);

			coreContext.OnCoordinatesChanged += CoordinatesChangedHandler;
			coreContext.OnAngleChanged += AngleChangedHandler;
			coreContext.OnSpeedChanged += SpeedChangedHandler;
			coreContext.OnLaserCountChanged += LaserCountChangedHandler;
			coreContext.OnLaserRestoreTimeChanged += LaserRestoreTimeChangedHandler;

			Show();
		}

		public override void Close()
		{
			_coreContext.OnCoordinatesChanged -= CoordinatesChangedHandler;
			_coreContext.OnAngleChanged -= AngleChangedHandler;
			_coreContext.OnSpeedChanged -= SpeedChangedHandler;
			_coreContext.OnLaserCountChanged -= LaserCountChangedHandler;
			_coreContext.OnLaserRestoreTimeChanged -= LaserRestoreTimeChangedHandler;

			base.Close();
		}

		private void CoordinatesChangedHandler()
		{
			SetText(_coordinates, _coreContext.Coordinates, null);
		}

		private void AngleChangedHandler()
		{
			SetText(_angle, _coreContext.Angle, FLOAT_FORMAT);
		}

		private void SpeedChangedHandler()
		{
			SetText(_speed, _coreContext.Speed, FLOAT_FORMAT);
		}

		private void LaserCountChangedHandler()
		{
			SetText(_laserCount, _coreContext.LaserCount, INT_FORMAT);
		}

		private void LaserRestoreTimeChangedHandler()
		{
			SetText(_laserRestoreTime, _coreContext.LaserRestoreTime, FLOAT_FORMAT);
		}

		private void SetText<TValue>(TMP_Text textField, TValue value, string format)
		{
			textField.text = string.IsNullOrEmpty(format) ? value.ToString() : string.Format(format, value);
		}
	}
}
