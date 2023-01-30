using System;

namespace RhoMicro.Domain
{
	public interface IDto
	{
		DateTimeOffset FirstUpdate { get; set; }
		DateTimeOffset LastUpdate { get; set; }
		Guid Id { get; set; }
	}
}