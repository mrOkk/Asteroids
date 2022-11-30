using System.Threading.Tasks;

namespace Interfaces.Services
{
	public interface IInitializable
	{
		Task InitializationTask { get; }
	}
}
