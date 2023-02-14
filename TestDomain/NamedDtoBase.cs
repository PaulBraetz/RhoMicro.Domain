using RhoMicro.Common.System;
using RhoMicro.Domain.Abstractions;

namespace TestDomain
{
    internal abstract class NamedDtoBase:DtoBase
    {
        internal NamedDtoBase(NamedEntityBase entity):base(entity) 
        {
            Name = entity.Name;
        }
        protected NamedDtoBase() { }

        public ValueString Name { get; set; }
    }
}
