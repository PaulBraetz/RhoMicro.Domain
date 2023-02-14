using RhoMicro.Domain;
using RhoMicro.Domain.Abstractions;

namespace TestDomain
{
    internal sealed class PropertyDto : NamedDtoBase
    {
        public PropertyDto()
        {
        }

        public PropertyDto(PropertyEntity entity, IDomain domain) : base(entity)
        {
            Owner = entity.Owner?.Release<PersonDto>(domain);
        }

        public PersonDto? Owner { get; set; }
    }
}
