using System;
using System.Linq;
using ZimLabs.Mapper;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Both classes are known...");
            var sourcePerson = new SourcePerson
            {
                Id = null,
                FirstName = "Bender",
                LastName = "Rodriguez",
                IgnoreValue = "Some fancy value",
                OtherId = 666
            };

            var targetPerson = new TargetPerson();

            Mapper.Map(targetPerson, sourcePerson);

            Console.WriteLine($"Source: {sourcePerson}");
            Console.WriteLine($"Target: {targetPerson}");

            Console.WriteLine("Only source class is known...");

            var result = Mapper.CreateAndMap<TargetPerson, SourcePerson>(sourcePerson);

            Console.WriteLine($"Source: {sourcePerson}");
            Console.WriteLine($"Target: {result}");

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }

    internal class TargetPerson
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [IgnoreProperty]
        public string IgnoreValue { get; set; }

        public int? OtherId { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; First name: {FirstName}; Last name: {LastName}; Ignore value: {IgnoreValue}; Other id: {OtherId}";
        }
    }

    internal class SourcePerson
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IgnoreValue { get; set; }
        public int OtherId { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; First name: {FirstName}; Last name: {LastName}; Ignore value: {IgnoreValue}; Other id: {OtherId}";
        }
    }
}
