using Core.WorldEntities;

namespace Core.Systems
{
	public class CollisionDetectionSystem : CoreSystem
	{
		public override void AddEntity(WorldEntity entity)
		{
			if (entity.HasComponent<CollisionDetectorLink>())
			{
				var collisionDetector = entity.GetComponent<CollisionDetectorLink>().CollisionDetector;
				collisionDetector.OnTriggerEnter += TriggerEnterHandler;
				collisionDetector.OnTriggerExit += TriggerExitHandler;
			}

			base.AddEntity(entity);
		}

		public override void RemoveEntity(WorldEntity entity)
		{
			if (entity.HasComponent<CollisionDetectorLink>())
			{
				var collisionDetector = entity.GetComponent<CollisionDetectorLink>().CollisionDetector;
				collisionDetector.OnTriggerEnter -= TriggerEnterHandler;
				collisionDetector.OnTriggerExit -= TriggerExitHandler;
			}

			base.RemoveEntity(entity);
		}

		public override void Run(float deltaTime)
		{
		}

		protected override bool Filter(WorldEntity entity) => false;

		private void TriggerEnterHandler(WorldEntity self, WorldEntity effector)
		{
			if (self.HasComponent<CollisionBehaviour>())
			{
				self.GetComponent<CollisionBehaviour>().CollisionHandler.Handle(self, effector, ECollisionEventType.Started);
			}
		}

		private void TriggerExitHandler(WorldEntity self, WorldEntity effector)
		{
			if (self.HasComponent<CollisionBehaviour>())
			{
				self.GetComponent<CollisionBehaviour>().CollisionHandler.Handle(self, effector, ECollisionEventType.Ended);
			}
		}
	}
}
