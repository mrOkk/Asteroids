using System.Threading.Tasks;
using UnityEngine;

namespace Interfaces.Services
{
	public interface IResourceLoader : IService
	{
		Task<TResource> LoadResource<TResource>(string path) where TResource : Object;
		void ReleaseResource(string path);
	}
}
