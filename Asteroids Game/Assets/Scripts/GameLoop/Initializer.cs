using GameConfigs;
using UI;
using UnityEngine;

namespace GameLoop
{
	public class Initializer : MonoBehaviour
	{
		[SerializeField]
		private GameConfig _gameConfig;
		[SerializeField]
		private UISystem _uiSystem;

		private void Awake()
		{
			var game = new Game(_gameConfig, _uiSystem);
			game.Run();
		}
	}
}
