using Core.Systems;
using Core.WorldEntities;
using CoreGameplay.Components;
using CoreGameplay.Components.Tags;
using Interfaces.UIContexts;
using UnityEngine;

namespace CoreGameplay.Systems
{
	public class UIUpdateSystem : CoreSystem
	{
		private readonly ICoreContext _coreContext;

		public UIUpdateSystem(ICoreContext coreContext)
		{
			_coreContext = coreContext;
		}

		public override void Run(float deltaTime)
		{
			foreach (var entity in Entities)
			{
				if (entity.HasComponent<TransformLink>())
				{
					var transform = entity.GetComponent<TransformLink>().Transform;
					_coreContext.Coordinates = transform.position;
					_coreContext.Angle = transform.eulerAngles.z;
				}

				if (entity.HasComponent<Movement>())
				{
					_coreContext.Speed = entity.GetComponent<Movement>().Speed;
				}

				if (entity.HasComponent<AlternativeShootAbility>())
				{
					var shootAbility = entity.GetComponent<AlternativeShootAbility>();
					_coreContext.LaserCount = shootAbility.Count;
					_coreContext.LaserRestoreTime = shootAbility.Count < shootAbility.MaxCount
						? shootAbility.RestoreTime - (Time.time - shootAbility.LastRestoredTime)
						: 0f;
				}
			}
		}

		protected override bool Filter(WorldEntity entity) => entity.HasComponent<PlayerTag>();
	}
}
