using UnityEngine;

namespace GameLoop
{
	public class Initializer : MonoBehaviour
	{
		private void Awake()
		{
			var game = new Game();
			game.Run();
		}
	}
}
