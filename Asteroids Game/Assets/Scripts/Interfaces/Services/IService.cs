using System.Threading.Tasks;

namespace Interfaces.Services
{
	public interface IService
	{
		Task InitializationTask { get; }
	}
}
