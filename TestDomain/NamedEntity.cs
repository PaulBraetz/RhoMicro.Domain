using RhoMicro.Domain.Abstractions;
using RhoMicro.Domain.ObjectSync.Synchronization;
using RhoMicro.ObjectSync.Attributes;

namespace TestDomain
{
    [SynchronizationTarget(
        BaseContextTypeName = nameof(EntityBaseSynchronizationContext),
        ContextTypeIsSealed = false,
        ContextTypeAccessibility = Accessibility.Protected,
        ContextPropertyAccessibility = Accessibility.Protected,
        ContextPropertyModifier = Modifier.Override)]
    internal abstract partial class NamedEntityBase:EntityBase
    {
        [Synchronized(Observable = true, PropertyAccessibility = Accessibility.Public)]
        private RhoMicro.Common.System.ValueString _name;

        protected NamedEntityBase(NamedDtoBase dto, ISynchronizationAuthority synchronizationAuthority) : base(dto, synchronizationAuthority)
        {
            Name = dto.Name;
        }
    }
}
