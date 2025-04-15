namespace SOLID;

/*
The main point is to avoid large interfaces. This will be done by segregating interfaces so there is no need to
implement interfaces that you don't need.
*/

public class InterfaceSegregation
{
    public InterfaceSegregation()
    {
        Console.WriteLine("I am the Interface Segregation Principle.\n");
        var printer = new SimplePrinter();
        printer.Print();

        var multiFunctionPrinter = new MultiFunctionPrinter();
        multiFunctionPrinter.Print();
        multiFunctionPrinter.Scan();
        multiFunctionPrinter.Fax();
    }
}

// BAD EXAMPLE:
public interface IMultiFunctionDevice
{
    void Print();
    void Scan();
    void Fax();
}

public class SimplePrinterBad : IMultiFunctionDevice
{
    public void Print()
    {
        Console.WriteLine("Printing...");
    }

    public void Scan()
    {
        // This is not a printer, so we shouldn't have to implement this method.
        throw new NotImplementedException();
    }

    public void Fax()
    {
        // This is not a printer, so we shouldn't have to implement this method.
        throw new NotImplementedException();
    }
}

// HOW WE FIX IT (by having more, smaller interfaces):
public interface IPrinter
{
    void Print();
}

public interface IScanner
{
    void Scan();
}

public interface IFax
{
    void Fax();
}

public class MultiFunctionPrinter : IPrinter, IScanner, IFax
{
    public void Print()
    {
        Console.WriteLine("Printing...");
    }

    public void Scan()
    {
        Console.WriteLine("Scanning...");
    }

    public void Fax()
    {
        Console.WriteLine("Faxing...");
    }
}

public class SimplePrinter : IPrinter
{
    public void Print()
    {
        Console.WriteLine("Printing...");
    }
}