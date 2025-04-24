namespace Builder;

public class FacetedBuilderDemo
{
    public FacetedBuilderDemo()
    {
        Console.WriteLine("I am the Faceted Builder Demo");
        var pb = new PersonBuilder();
        Person person = pb
            .Works
            .At("Home")
            .AsA("Developer")
            .Earning(0)
            .Lives
            .At("Coffe lane")
            .In("Bugland")
            .WithPostcode(1337);
        Console.WriteLine(person);
    }

    private class Person
    {
        // Address
        public string StreetAddress, City;

        public int Postcode;

        // Employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return
                $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(City)}: {City}, {nameof(Postcode)}: {Postcode}, " +
                $"{nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    private class PersonBuilder // Facade
    {
        // Reference!
        protected Person person = new();

        public PersonJobBuilder Works => new(person);
        public PersonAddressBuilder Lives => new(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    private class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int annualIncome)
        {
            person.AnnualIncome = annualIncome;
            return this;
        }
    }

    private class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }

        public PersonAddressBuilder WithPostcode(int postcode)
        {
            person.Postcode = postcode;
            return this;
        }
    }
}