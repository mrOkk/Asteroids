using UnityEngine;

namespace Interfaces.Services
{
	public interface ICameraService : IService
	{
		Camera MainCamera { get; }
		Vector2 WorldScreenSize { get; }

		void UpdateWorldScreenSize();
	}
}
