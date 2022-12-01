using CoreGameplay.Components;
using CoreGameplay.Components.Tags;
using CoreSystem;
using GameConfigs.Configs;
using Interfaces.Services;

namespace CoreGameplay.Systems
{
	public class InputHandlingSystem : CoreSystem.CoreSystem
	{
		private readonly IInputService _inputService;
		private readonly PlayerConfig _playerConfig;

		public InputHandlingSystem(IInputService inputService, PlayerConfig playerConfig)
		{
			_inputService = inputService;
			_playerConfig = playerConfig;
		}

		public override void Run(float deltaTime)
		{
			foreach (var entity in Entities)
			{
				if (entity.HasComponent<Acceleration>())
				{
					entity.GetComponent<Acceleration>().Value = _inputService.Accelerate
						? _playerConfig.Acceleration
						: 0f;
				}

				if (entity.HasComponent<RotationSpeed>())
				{
					var hasRotation = _inputService.RotateLeft ^ _inputService.RotateRight;
					var rotationSign = hasRotation
						? _inputService.RotateLeft ? 1f : -1f
						: 0f;
					entity.GetComponent<RotationSpeed>().Value = rotationSign * _playerConfig.RotationSpeed;
				}

				if (entity.HasComponent<ShootAbility>())
				{
					entity.GetComponent<ShootAbility>().Requested = _inputService.Fire;
				}

				if (entity.HasComponent<AlternativeShootAbility>())
				{
					entity.GetComponent<AlternativeShootAbility>().Requested = _inputService.AlternativeFire;
				}
			}
		}

		protected override bool Filter(WorldEntity entity)
			=> entity.HasComponent<PlayerTag>();
	}
}
