using System;
using ZimLabs.Mapper;
using ZimLabs.TableCreator;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            TestOne();
            Console.WriteLine();

            TestTwo();
            Console.WriteLine();

            TestThree();
            Console.WriteLine();

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

            Console.WriteLine("Source:");
            Console.WriteLine(sourcePerson.CreateValueTable());
            Console.WriteLine();
            Console.WriteLine("Target:");
            Console.WriteLine(targetPerson.CreateValueTable());
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

            Console.WriteLine("Source:");
            Console.WriteLine(sourcePerson.CreateValueTable());
            Console.WriteLine();
            Console.WriteLine("Target:");
            Console.WriteLine(targetPerson.CreateValueTable());
        }

        private static void TestThree()
        {
            Console.WriteLine("Get changes between two objects");

            var firstPerson = new TargetPerson
            {
                Id = 1,
                FirstName = "Bender",
                LastName = "Rodriguez",
                IgnoreValue = "Some fancy value",
                OtherId = 666
            };

            var secondPerson = new TargetPerson
            {
                Id = 1,
                FirstName = "Philip J.",
                LastName = "Fry",
                IgnoreValue = "Some fancy value",
                OtherId = 123
            };

            var changes = firstPerson.GetChanges(secondPerson);

            Console.WriteLine(changes.CreateTable());
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
