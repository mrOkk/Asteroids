using Interfaces.UIContexts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
	public class ResultScreen : ScreenBase
	{
		[SerializeField]
		private Button _restartButton;
		[SerializeField]
		private TMP_Text _result;

		private IResultContext _resultContext;

		public void Show(IResultContext resultContext)
		{
			_resultContext = resultContext;
			_result.text = resultContext.Result.ToString("0");

			Show();
		}

		private void Awake()
		{
			_restartButton.onClick.AddListener(Restart);
		}

		private void Restart()
		{
			_resultContext.Restart();
		}
	}
}
