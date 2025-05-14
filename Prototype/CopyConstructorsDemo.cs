namespace Prototype;

/*
C# has a ICopyable interface that can be implemented to create a copy of an object. However, this is only
creating a shallow copy. This means that a Clone returns a generic object, needing to be cast to the correct class,
and that all the objects attributes are copied by reference.

Instead, we can create a copy constructor that takes an object of the same class and copies the values of the
attributes.This is a deep copy, meaning that all the objects attributes are copied by value.

This concept is borrowed from C++ and is not that common in C#.
*/

public class CopyConstructorsDemo
{
    public CopyConstructorsDemo()
    {
        Console.WriteLine("I am the CopyConstructorDemo program");
        var person = new Person("John Doe", new Address("Main St", 123));
        var person2 = new Person(person)
        {
            Name = "Jane Doe",
            Address =
            {
                StreetName = "Second St",
                HouseNumber = 456
            }
        };
        Console.WriteLine(person);
        Console.WriteLine(person2);
    }

    private class Person
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public Person(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        public Person(Person other)
        {
            Name = other.Name;
            Address = new Address(other.Address);
        }

        public override string ToString()
        {
            return $"Name: {Name}, Address: {Address.StreetName} {Address.HouseNumber}";
        }
    }

    private class Address
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public Address(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }
    }
}