using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Extensions
{
	public static class TaskExtensions
	{
		private static readonly Action<Task> _handleFinishedTask = HandleFinishedTask;

		public static void Forget(this Task task)
		{
			task.ContinueWith(_handleFinishedTask);
		}

		private static void HandleFinishedTask(Task task)
		{
			if (task.IsFaulted)
			{
				Debug.LogException(task.Exception);
			}
		}
	}
}
