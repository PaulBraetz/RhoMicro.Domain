using Microsoft.Extensions.Logging;

using RhoMicro.Domain;

namespace TestDomain
{
    internal class Program
    {
        static void Main(String[] args)
        {
            var domain = new Domain();

            var firstOwner = new PersonDto(DateTimeOffset.Parse("20.05.1979"))
            {
                Name = "Mary"
            }.Capture<PersonEntity>(domain);
            var secondOwner = new PersonDto(DateTimeOffset.Parse("15.11.1956"))
            {
                Name = "Jake"
            }.Capture<PersonEntity>(domain);

            var property = new PropertyDto()
            {
                Name = "Car",
                Owner = firstOwner.Release<PersonDto>(domain)
            }.Capture<PropertyEntity>(domain);

            var propertyClone = property.Release<PropertyDto>(domain).Capture<PropertyEntity>(domain);

            property.Owner = secondOwner;

            Console.WriteLine(property.Owner.Name);
            Console.WriteLine(propertyClone.Owner.Name);
        }
    }
}