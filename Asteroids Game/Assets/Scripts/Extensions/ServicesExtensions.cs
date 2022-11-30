﻿using System.Threading.Tasks;
using Interfaces.Services;

namespace Extensions
{
	public static class ServicesExtensions
	{
		public static async Task<TService> WaitForServiceReady<TService>(this TService instance) where TService : IInitializable
		{
			await instance.InitializationTask;
			return instance;
		}
	}
}
