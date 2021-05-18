using Orleans;
using System;
using System.Threading.Tasks;

namespace OE.Silo.Abstractions
{
	public interface IOutboxGrain : IGrainWithGuidKey
	{
		Task SendOutboxItem(Guid id);
	}
}
