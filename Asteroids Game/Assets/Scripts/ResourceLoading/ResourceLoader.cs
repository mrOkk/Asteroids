using System.Collections.Generic;
using System.Threading.Tasks;
using Interfaces.Services;
using UnityEngine;

namespace ResourceLoading
{
	public class ResourceLoader : IResourceLoader
	{
		public Task InitializationTask => Task.CompletedTask;

		private readonly Dictionary<string, Object> _loadedAssets = new();

		public async Task<TResource> LoadResource<TResource>(string path) where TResource : Object
		{
			if (_loadedAssets.TryGetValue(path, out var asset))
			{
				return (TResource)asset;
			}

			var request = Resources.LoadAsync<TResource>(path);
			if (!request.isDone)
			{
				await Task.Yield();
			}

			return (TResource)request.asset;
		}

		public void ReleaseResource(string path)
		{
			if (!_loadedAssets.TryGetValue(path, out var asset))
			{
				Debug.LogError($"No asset for such path: {path}");
			}

			Resources.UnloadAsset(asset);
			_loadedAssets.Remove(path);
		}
	}
}
