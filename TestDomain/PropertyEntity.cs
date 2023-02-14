using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fort;

using RhoMicro.Domain;
using RhoMicro.Domain.Abstractions;
using RhoMicro.Domain.ObjectSync.Synchronization;
using RhoMicro.ObjectSync.Attributes;

namespace TestDomain
{

    [SynchronizationTarget(
        BaseContextTypeName = nameof(NamedEntityBaseSynchronizationContext),
        ContextTypeIsSealed = false,
        ContextTypeAccessibility = Accessibility.Protected,
        ContextPropertyAccessibility = Accessibility.Protected,
        ContextPropertyModifier = Modifier.Override)]
    internal sealed partial class PropertyEntity : NamedEntityBase
    {
        private PropertyEntity(PropertyDto dto, ISynchronizationAuthority synchronizationAuthority) : base(dto, synchronizationAuthority)
        {
        }

        public static PropertyEntity Create(PropertyDto dto, IDomain domain, ISynchronizationAuthority synchronizationAuthority)
        {
            dto.ThrowIfNull(nameof(dto));
            domain.ThrowIfNull(nameof(domain));
            synchronizationAuthority.ThrowIfNull(nameof(synchronizationAuthority));

            var result = new PropertyEntity(dto, synchronizationAuthority)
            {
                Owner = dto.Owner?.Capture<PersonEntity>(domain)
            };

            return result;
        }

        [Synchronized(Observable = true)]
        private PersonEntity? _owner;
    }
}
