# ZimLabs.Mapper
 Easy way to map two objects into one

![Nuget](https://img.shields.io/nuget/v/ZimLabs.Mapper) ![GitHub release (latest by date)](https://img.shields.io/github/v/release/InvaderZim85/ZimLabs.Mapper)

## Usage

The two objects:

1. The objects

    ```csharp
    internal class TargetPerson
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Ignore this property!
        [IgnoreProperty]
        public string IgnoreValue { get; set; }

        public int? OtherId { get; set; }
    }

    internal class SourcePerson
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IgnoreValue { get; set; }
        public int OtherId { get; set; }
    }
    ```

2. Map two objects (both objects exists)

    ```csharp
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
    ```

3. Map two objects but create a new target object

    ```csharp
    var sourcePerson = new SourcePerson
    {
        Id = null,
        FirstName = "Bender",
        LastName = "Rodriguez",
        IgnoreValue = "Some fancy value",
        OtherId = 666
    };

    var targetPerson = Mapper.CreateAndMap<SourcePerson, TargetPerson>(sourcePerson);
    ```