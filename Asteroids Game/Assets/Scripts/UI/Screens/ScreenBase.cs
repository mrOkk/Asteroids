using UnityEngine;

namespace UI.Screens
{
	public abstract class ScreenBase : MonoBehaviour
	{
		protected virtual void Show()
		{
			gameObject.SetActive(true);
		}

		public virtual void Close()
		{
			gameObject.SetActive(false);
		}
	}
}
