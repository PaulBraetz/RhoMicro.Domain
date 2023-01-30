using RhoMicro.Domain;

namespace TestDomain
{
	internal sealed class PersonDto : DtoBase
	{
		public String Name { get; set; }
        public PersonEntity ToEntity()=>new PersonEntity(this);
	}
}
