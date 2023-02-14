namespace TestDomain
{
    internal sealed class PersonDto : NamedDtoBase
    {
        public PersonDto()
        {
        }

        public PersonDto(DateTimeOffset birthday)
        {
            BirthDay = birthday;
        }

        public PersonDto(PersonEntity entity) : base(entity)
        {
            BirthDay = entity.BirthDay;
        }

        public DateTimeOffset BirthDay { get; private set; }
    }
}
