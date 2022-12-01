using System;
using Interfaces.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Core
{
	public class CameraService : ICameraService
	{
		private const string MAIN_CAMERA_TAG = "MainCamera";

		public Camera MainCamera { get; }

		private Vector2 _worldScreenSize;
		public Vector2 WorldScreenSize => _worldScreenSize;

		private int _targetScreenWidth;
		private int _targetScreenHeight;
		private float _targetRatio;

		public CameraService(Camera camera)
		{
			MainCamera = camera;
			Object.DontDestroyOnLoad(camera);
			SceneManager.sceneLoaded += SceneLoadedHandler;
		}

		public void UpdateWorldScreenSize()
		{
			var ratio = _targetRatio;
			var verticalSize = MainCamera.orthographicSize * 2;

			if (ratio == 0 || _targetScreenHeight != Screen.height)
			{
				_targetScreenHeight = Screen.height;
				ratio = _targetScreenHeight / verticalSize;
				_worldScreenSize.y = verticalSize;
			}

			if (Math.Abs(ratio - _targetRatio) > float.Epsilon || _targetScreenWidth != Screen.width)
			{
				_targetScreenWidth = Screen.width;
				_worldScreenSize.x = _targetScreenWidth / ratio;
			}

			_targetRatio = ratio;
		}

		private void SceneLoadedHandler(Scene scene, LoadSceneMode loadSceneMode)
		{
			var rootObjects = scene.GetRootGameObjects();

			foreach (var rootObject in rootObjects)
			{
				var allCameras = rootObject.GetComponentsInChildren<Camera>(true);

				foreach (var camera in allCameras)
				{
					if (MainCamera != camera && camera.CompareTag(MAIN_CAMERA_TAG))
					{
						Object.Destroy(camera);
					}
				}
			}
		}
	}
}
