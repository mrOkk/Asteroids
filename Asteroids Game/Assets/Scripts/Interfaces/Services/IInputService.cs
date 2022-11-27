namespace Interfaces.Services
{
	public interface IInputService : IService
	{
		bool Accelerate { get; }
		bool RotateRight { get; }
		bool RotateLeft { get; }
		bool Fire { get; }
		bool AlternativeFire { get; }

		void SetActive(bool active);
	}
}
