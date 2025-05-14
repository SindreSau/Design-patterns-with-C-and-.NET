namespace Prototype;

public class DeepCopyDemo
{
    public DeepCopyDemo()
    {
        Console.WriteLine("I am the DeepCopyDemo program");

        var person1 = new Person("John Doe", 30, new Address("123 Main St", "New York"));

        var person2 = person1.Clone();
        person2.Name = "Jane Doe";
        person2.Age = 25;
        person2.Address.Street = "456 Elm St";
        person2.Address.City = "Los Angeles";

        Console.WriteLine(person1);
        Console.WriteLine(person2);
    }

    private interface IPrototype<out T>
    {
        T Clone(); // This is now a DEEP COPY
    }

    private class Person : IPrototype<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }

        public Person(string name, int age, Address address)
        {
            Name = name;
            Age = age;
            Address = address;
        }

        public Person Clone()
        {
            // Create a deep copy of the Address object
            var clonedAddress = new Address(Address.Street, Address.City);
            // Return a new Person object with the cloned Address
            return new Person(Name, Age, clonedAddress);
        }

        public override string ToString()
        {
            return $"{Name}, {Age}, {Address}";
        }
    }

    private class Address : IPrototype<Address>
    {
        public string Street { get; set; }
        public string City { get; set; }

        public Address(string street, string city)
        {
            Street = street;
            City = city;
        }

        public Address Clone()
        {
            return new Address(Street, City);
        }

        public override string ToString()
        {
            return $"{Street}, {City}";
        }
    }
}