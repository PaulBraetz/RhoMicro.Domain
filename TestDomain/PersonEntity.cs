using Fort;
using RhoMicro.ObjectSync.Attributes;
using RhoMicro.Domain.Abstractions;
using static TestDomain.PersonEntity;
using RhoMicro.Domain.ObjectSync.Synchronization;
using RhoMicro.Domain;

namespace TestDomain
{
    [SynchronizationTarget(
        BaseContextTypeName = nameof(NamedEntityBaseSynchronizationContext),
        ContextTypeIsSealed = true,
        ContextTypeAccessibility = Accessibility.Protected,
        ContextPropertyAccessibility = Accessibility.Protected,
        ContextPropertyModifier = Modifier.Override)]
    internal sealed partial class PersonEntity : NamedEntityBase
    {
        private PersonEntity(PersonDto dto, ISynchronizationAuthority synchronizationAuthority) : base(dto, synchronizationAuthority)
        {
        }

        public static PersonEntity Create(PersonDto dto, IDomain domain, ISynchronizationAuthority synchronizationAuthority)
        {
            dto.ThrowIfNull(nameof(dto));
            domain.ThrowIfNull(nameof(domain));
            synchronizationAuthority.ThrowIfNull(nameof(synchronizationAuthority));

            var result = new PersonEntity(dto, synchronizationAuthority)
            {
                BirthDayInternal = dto.BirthDay
            };

            return result;
        }

        public DateTimeOffset BirthDay => BirthDayInternal;
        [Synchronized(Observable = true, PropertyAccessibility = Accessibility.Private)]
        private DateTimeOffset _birthDayInternal;
    }

}