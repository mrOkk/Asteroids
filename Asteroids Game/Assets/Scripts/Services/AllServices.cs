using Interfaces.Services;

namespace Services
{
	public class AllServices
	{
		private static class ServiceContainer<TService> where TService : IService
		{
			public static TService Instance;
		}

		public void RegisterSingle<TService>(TService instance) where TService : IService
			=> ServiceContainer<TService>.Instance = instance;

		public TService GetSingle<TService>() where TService : IService
			=> ServiceContainer<TService>.Instance;
	}
}
