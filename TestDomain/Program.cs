namespace TestDomain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var person1 = new PersonEntity("Jake");
            person1.PropertyChanged += (s, e) => Console.WriteLine($"person1.{e.PropertyName} has changed to {person1.Name}");

            var dto = person1.ToDto();
            var person2 = dto.ToEntity();
            person2.PropertyChanged += (s, e) => Console.WriteLine($"person2.{e.PropertyName} has changed to {person2.Name}");

            Console.WriteLine("Expecting Jake x2");
            Console.WriteLine(person1.Name);
            Console.WriteLine(person2.Name);

            person1.Name = "Todd";
            Console.WriteLine("Expecting Todd x2");
            Console.WriteLine(person1.Name);
            Console.WriteLine(person2.Name);
        }
    }
}