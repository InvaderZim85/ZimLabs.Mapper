using System;
using System.Linq;
using ZimLabs.Mapper;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            TestOne();

            TestTwo();

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void TestOne()
        {
            // Both types are existing and not null
            Console.WriteLine("Both types are not null");

            var sourcePerson = new SourcePerson
            {
                Id = null,
                FirstName = "Bender",
                LastName = "Rodriguez",
                IgnoreValue = "Some fancy value",
                OtherId = 666
            };

            var targetPerson = new TargetPerson();

            Mapper.Map(sourcePerson, targetPerson);

            Console.WriteLine($"Source: {sourcePerson}");
            Console.WriteLine($"Target: {targetPerson}");
        }

        private static void TestTwo()
        {
            Console.WriteLine("The source type is not null and create a new target");

            var sourcePerson = new SourcePerson
            {
                Id = null,
                FirstName = "Bender",
                LastName = "Rodriguez",
                IgnoreValue = "Some fancy value",
                OtherId = 666
            };

            var targetPerson = Mapper.CreateAndMap<SourcePerson, TargetPerson>(sourcePerson);

            Console.WriteLine($"Source: {sourcePerson}");
            Console.WriteLine($"Target: {targetPerson}");
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
