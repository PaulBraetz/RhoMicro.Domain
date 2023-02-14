using Fort;

using Microsoft.Extensions.Logging;

using RhoMicro.Domain;
using RhoMicro.Domain.Abstractions;
using RhoMicro.Domain.ObjectSync.Synchronization;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDomain
{
    internal sealed class Domain : IDomain
    {
        public Domain()
        {
            _synchronizationAuthority = new ConsoleMemorySynchronizationAuthority();
        }

        public IEntity Capture(IDto dto)
        {
            dto.ThrowIfNull(nameof(dto));

            var dtoType = dto.GetType();
            var result = _captureFactories.TryGetValue(dtoType, out var factory) ?
                factory.Invoke(dto, this, _synchronizationAuthority) :
                throw new DomainCaptureException(dto, this);

            return result;
        }

        public IDto Release(IEntity entity)
        {
            entity.ThrowIfNull(nameof(entity));

            var entityType = entity.GetType();
            var result = _releaseFactories.TryGetValue(entityType, out var factory) ?
                factory.Invoke(entity, this) :
                throw new DomainReleaseException(entity, this);

            return result;
        }

        private static readonly IDictionary<Type, Func<IDto, IDomain, ISynchronizationAuthority, IEntity>> _captureFactories = new Dictionary<Type, Func<IDto, IDomain, ISynchronizationAuthority, IEntity>>()
        {
            {typeof(PersonDto), (o, d, a)=>PersonEntity.Create((PersonDto)o, d, a) },
            {typeof(PropertyDto), (o, d, a)=>PropertyEntity.Create((PropertyDto)o, d, a) },
        };
        private static readonly IDictionary<Type, Func<IEntity, IDomain, IDto>> _releaseFactories = new Dictionary<Type, Func<IEntity, IDomain, IDto>>()
        {
            {typeof(PersonEntity), (e, d)=>new PersonDto((PersonEntity)e) },
            {typeof(PropertyEntity), (e, d)=>new PropertyDto((PropertyEntity)e, d) },
        };

        private readonly ISynchronizationAuthority _synchronizationAuthority;
    }
}
