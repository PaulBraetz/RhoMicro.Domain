using System;

namespace RhoMicro.Domain
{
	public abstract class DtoBase : IDto
	{
		public DateTimeOffset FirstUpdate { get; set; }
		public DateTimeOffset LastUpdate { get; set; }
		public Guid Id { get; set; }
	}
}