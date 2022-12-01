using System;
using CoreSystem;
using UnityEngine;

namespace Core
{
	public class CollisionDetector : MonoBehaviour
	{
		public event Action<WorldEntity, WorldEntity> OnTriggerEnter;
		public event Action<WorldEntity, WorldEntity> OnTriggerExit;

		[SerializeField]
		private EntityView _entityView;

		private void OnTriggerEnter2D(Collider2D other)
		{
			var otherView = other.GetComponent<EntityView>();

			if (otherView == null)
			{
				Debug.LogError("There is a collider on an object, but no EntityView");
				return;
			}

			OnTriggerEnter?.Invoke(_entityView.WorldEntity, otherView.WorldEntity);
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			var otherView = other.GetComponent<EntityView>();

			if (otherView == null)
			{
				Debug.LogError("There is a collider on an object, but no EntityView");
				return;
			}

			OnTriggerExit?.Invoke(_entityView.WorldEntity, otherView.WorldEntity);
		}
	}
}
