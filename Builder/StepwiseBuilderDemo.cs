namespace Builder;

/*
For this builder implementation, the interface returned from each build step
is a different interface. This enforces the order of the steps:
1. Specify the car type
2. Specify the wheel size
3. Build the car
*/

public class StepwiseBuilderDemo
{
    public StepwiseBuilderDemo()
    {
        Console.WriteLine("I am the stepwise builder program.");

        var car = CarBuilder
            .Create()
            .OfType(CarType.Sedan)
            .WithWheels(16)
            .Build();
        Console.WriteLine(car);

        var car2 = CarBuilder
            .Create()
            .OfType(CarType.Crossover)
            .WithWheels(18)
            .Build();
        Console.WriteLine(car2);
    }
}

internal enum CarType
{
    Sedan,
    Crossover
}

internal class Car
{
    public CarType Type;
    public int WheelSize;

    public override string ToString()
    {
        return $"Car type: {Type}, Wheel size: {WheelSize}";
    }
}

internal interface ISpecifyCarType
{
    ISpecifyWheelSize OfType(CarType type);
}

internal interface ISpecifyWheelSize
{
    IBuildCar WithWheels(int size);
}

internal interface IBuildCar
{
    public Car Build();
}

internal class CarBuilder
{
    private class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
    {
        private readonly Car _car = new();

        public ISpecifyWheelSize OfType(CarType type)
        {
            _car.Type = type;
            return this;
        }

        public IBuildCar WithWheels(int size)
        {
            switch (_car.Type)
            {
                case CarType.Crossover when size is < 17 or > 20:
                case CarType.Sedan when size is < 15 or > 17:
                    throw new ArgumentException($"Wrong size of wheel for {_car.Type}");
            }

            _car.WheelSize = size;
            return this;
        }

        public Car Build()
        {
            return _car;
        }
    }

    public static ISpecifyCarType Create()
    {
        return new Impl();
    }
}