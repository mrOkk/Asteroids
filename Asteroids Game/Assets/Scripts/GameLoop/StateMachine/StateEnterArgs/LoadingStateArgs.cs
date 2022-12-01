namespace GameLoop.StateMachine.StateEnterArgs
{
	public class LoadingStateArgs : StateEnterArgs
	{
		public string SceneName { get; }

		public LoadingStateArgs(string sceneName)
		{
			SceneName = sceneName;
		}
	}
}
