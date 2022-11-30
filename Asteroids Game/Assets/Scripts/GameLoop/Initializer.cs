using Configs;
using UnityEngine;

namespace GameLoop
{
	public class Initializer : MonoBehaviour
	{
		[SerializeField]
		private GameConfig _gameConfig;

		private void Awake()
		{
			var game = new Game(_gameConfig);
			game.Run();
		}
	}
}
