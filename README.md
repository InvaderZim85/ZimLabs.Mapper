# ZimLabs.Mapper
 Easy way to map two objects into one

![Nuget](https://img.shields.io/nuget/v/ZimLabs.Mapper) ![GitHub release (latest by date)](https://img.shields.io/github/v/release/InvaderZim85/ZimLabs.Mapper)

## Usage

### Mapping

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

### Get changes

1. The objects

    ```csharp
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
    ```

2. Get the changes

    ```csharp
    var changes = firstPerson.GetChanges(secondPerson);
    ```

    The variable `change` contains a list with all changes. Every entry contains the following properties:
    - *Property*: The name of the property
    - *OldValue* The "original" old value
    - *NewValue* The "new" value

3. The result

    ```
    +-----------+-----------+-----------+
    | Property  | OldValue  | NewValue  |
    +-----------+-----------+-----------+
    | FirstName | Bender    | Philip J. |
    | LastName  | Rodriguez | Fry       |
    | OtherId   | 666       | 123       |
    +-----------+-----------+-----------+
    ```
