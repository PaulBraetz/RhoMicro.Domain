using RhoMicro.ObjectSync.Attributes;
using RhoMicro.Domain.ObjectSync.Synchronization;
using Fort;
using System.ComponentModel;
using System;

namespace RhoMicro.Domain
{
	[SynchronizationTarget(
		ContextTypeIsSealed = false,
		ContextTypeAccessibility = Accessibility.Protected,
		ContextTypeConstructorAccessibility = Accessibility.Public,
		ContextPropertyAccessibility = Accessibility.Protected,
		ContextPropertyModifier = Modifier.Virtual)]
	public abstract partial class EntityBase<TDto> : IEntity<TDto>
		where TDto : IDto
	{
		protected EntityBase(TDto dto)
		{
			dto.ThrowIfDefault(nameof(dto));

			FirstUpdate = dto.FirstUpdate;
			LastUpdate = dto.LastUpdate;
			Id = dto.Id;
			SourceInstanceId = Id.ToString();

			SynchronizationContext.Synchronize();
		}
		protected EntityBase()
		{
			FirstUpdate = DateTimeOffset.UtcNow;
			LastUpdate = DateTimeOffset.UtcNow;
			Id = Guid.NewGuid();
			SourceInstanceId = Id.ToString();

			SynchronizationContext.Synchronize();
		}

		public DateTimeOffset FirstUpdate { get; private set; } = DateTimeOffset.UtcNow;
		public DateTimeOffset LastUpdate { get; private set; }
		public Guid Id { get; private set; }

		[SourceInstanceId]
		private String SourceInstanceId { get; }

		[SynchronizationAuthority]
		protected virtual ISynchronizationAuthority Authority { get; } = StaticSynchronizationAuthority.Instance;

		public event PropertyChangedEventHandler? PropertyChanged;

		public abstract TDto ToDto();

		protected void OnPropertyChanged(String propertyName)
		{
			propertyName.ThrowIfDefaultOrEmpty(nameof(propertyName));
			LastUpdate = DateTimeOffset.UtcNow;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}