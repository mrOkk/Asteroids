using System;
using UnityEngine;

namespace DebugScripts
{
	public class ScreenSizeFitter : MonoBehaviour
	{
		public BoxCollider2D BoxCollider;

		private void Awake()
		{
			var mainCamera = Camera.main;
			var innerSize = mainCamera.orthographicSize * 2;
			var sizeToPixelRatio = Screen.height / innerSize;
			BoxCollider.size = new Vector2(Screen.width / sizeToPixelRatio, innerSize);
		}
	}
}
