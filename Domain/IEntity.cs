using System;
using System.ComponentModel;

namespace RhoMicro.Domain
{
	public interface IEntity<TDto>: INotifyPropertyChanged
		where TDto : IDto
	{
		DateTimeOffset FirstUpdate { get; }
		DateTimeOffset LastUpdate { get; }
		Guid Id { get; }
		TDto ToDto();
	}
}